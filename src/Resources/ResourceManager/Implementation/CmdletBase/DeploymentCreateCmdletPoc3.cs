//using Azure.Deployments.Core;
//using Azure.Deployments.Engine;
//using Azure.Deployments.Engine.Host.Azure;
//using Azure.Deployments.Core.Constants;

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Implementation.CmdletBase
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
    using Microsoft.Azure.Management.Resources.Models;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public abstract partial class DeploymentCreateCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "File to write the map of marked properties to that will be used for subsequent deployments.")]
        public string Poc3MarkedPropertiesStorageFile { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to enable POC3 noise reduction if whatif flag is also provided.")]
        public SwitchParameter Poc3WhatIf { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 1 of POC3 noise reduction, or the saving of the marked properties to an external file (db).")]
        public SwitchParameter Poc3SaveNoise { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "If the deployment failed, the properties are added instead of overwriting previous properties.")]
        public SwitchParameter Poc3SaveNoiseDeploymentFailed { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 2 of POC3 noise reduction, or the loading of the marked properties to an external file (db) and canceling out of noise.\"")]
        public SwitchParameter Poc3IngestNoise { get; set; }

        private void Poc3(WhatIfOperationResult whatIfOperationResult)
        {
            // Ensure only one step is being handeled per call. Poc3 requires 2 invocations of the cmdelt.
            if (!(Poc3SaveNoise.IsPresent || Poc3IngestNoise.IsPresent) || (Poc3SaveNoise.IsPresent && Poc3IngestNoise.IsPresent))
            {
                throw new Exception("Must have only 1 of Poc1SaveNoise and Poc1IngestNoise set when running Poc3WhatIf.");
            }
            // Ensure storage file path is provided.
            if (Poc3MarkedPropertiesStorageFile == null || Poc3MarkedPropertiesStorageFile == "")
            {
                throw new Exception("The Poc3MarkedPropertiesStorageFile parameter cannot be empty when running Poc3WhatIf.");
            }

            // Mark all resource parameters that are explictly defined in the template.
            var marked = new Dictionary<string, bool>();
            var resources = ExtractResourcesFromTemplate();

            // Go through each resource in the template and mark both the overall resource and each property path
            // as explictly being defined.
            foreach (var resource in resources)
            {
                var resourceProperties = resource["properties"];
                var resourceName = resource["name"].ToString();
                var resourceType = resource["type"].ToString();

                // Handle functions.
                if (resourceName.Contains("["))
                {
                    resourceName = HandleFunctionInName(resourceName);
                }

                // Mark the resource as a whole in case resources are implictly created. We don't want to include implicitly created
                // resources, even if there were no changes.
                marked[resourceName] = true;

                MarkProperties(resourceProperties, resourceName, marked);
            }

            //var engine = new AzureDeploymentEngine();


            if (Poc3SaveNoise.IsPresent)
            {
                if (Poc3SaveNoiseDeploymentFailed.IsPresent)
                {
                    IngestPreviouslyMarkedProperties(marked, Poc3MarkedPropertiesStorageFile);
                }

                // Save properties to simulate saving on deployment.
                SaveMarkedProperties(marked, Poc3MarkedPropertiesStorageFile);
                this.WriteDebug("POC1: Saved Noise file!");
            }
            else if (Poc3IngestNoise.IsPresent)
            {
                // Mark properties that were explicitly set in the previous deployment.
                IngestPreviouslyMarkedProperties(marked, Poc3MarkedPropertiesStorageFile);

                // Remove noise in changes and update the whatIf result.
                var updatedChanges = RemoveNoise(whatIfOperationResult.Changes, marked);
                whatIfOperationResult.Changes = updatedChanges;
            }
        }

        private void SaveMarkedProperties(IDictionary<string, bool> marked, string markedPropertiesStorageFile)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(markedPropertiesStorageFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, marked);
            }
        }

        private void IngestPreviouslyMarkedProperties(IDictionary<string, bool> markedProperties, string markedPropertiesStorageFile)
        {
            // Read in the previously marked properties
            Dictionary<string, bool> previouslyMarkedProperties;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(markedPropertiesStorageFile))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                previouslyMarkedProperties = serializer.Deserialize<Dictionary<string, bool>>(reader);
            }

            // Add them to the marked map.
            foreach (var property in previouslyMarkedProperties.Keys) 
            {
                markedProperties[property] = true;
            }
        }
    }
}
