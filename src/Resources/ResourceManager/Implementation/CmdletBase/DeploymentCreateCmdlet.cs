// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Implementation.CmdletBase
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.Common.Strategies;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Formatters;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Properties;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.Deployments;
    using Microsoft.Azure.Management.Resources.Models;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using System.Text.RegularExpressions;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using JsonDiffPatchDotNet;
    using Newtonsoft.Json;
    using System.Collections;

    public abstract class DeploymentCreateCmdlet : DeploymentWhatIfCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "The query string (for example, a SAS token) to be used with the TemplateUri parameter. Would be used in case of linked templates")]
        public string QueryString { get; set; }

        protected abstract ConfirmImpact ConfirmImpact { get; }

        /// <summary>
        /// It's important not to call this function more than once during an invocation, as it can call the Bicep CLI.
        /// This is slow, and can also cause diagnostics to be emitted multiple times.
        /// </summary>
        protected abstract PSDeploymentCmdletParameters BuildDeploymentParameters();

        protected abstract bool ShouldSkipConfirmationIfNoChange();

        protected override void OnProcessRecord()
        {
            string whatIfMessage = null;
            string warningMessage = null;
            string captionMessage = null;

            PSWhatIfOperationResult whatIfResult = null;

            if (this.ShouldExecuteWhatIf())
            {
                whatIfResult = this.ExecuteWhatIf();

                // -----------------------------------------------------------------------------------------------------------------------------------------------

                // Only run POC code if what if succeeded
                if (whatIfResult.Error == null && whatIfResult.Status == "Succeeded")
                {
                    if (Poc1WhatIf.IsPresent)
                    {
                        // Ensure only one step is being handeled per call. Poc1 requires 2 invocations of the cmdelt.
                        if (!(Poc1SaveNoise.IsPresent || Poc1IngestNoise.IsPresent) || (Poc1SaveNoise.IsPresent && Poc1IngestNoise.IsPresent))
                        {
                            throw new Exception("Must have only 1 of Poc1SaveNoise and Poc1IngestNoise set when running Poc1WhatIf.");
                        }
                        // Ensure storage file path is provided.
                        if (Poc1NoiseStorageFile == null || Poc1NoiseStorageFile == "")
                        {
                            throw new Exception("The Poc1StorageFile parameter cannot be empty when running Poc1WhatIf.");
                        }

                        var whatIfOperationResult = whatIfResult.whatIfOperationResult;

                        if (Poc1SaveNoise.IsPresent)
                        {
                            // Save noise to provided file.
                            SaveNoise(whatIfOperationResult.Changes, Poc1NoiseStorageFile);
                            this.WriteDebug("POC1: Saved Noise file!");
                            
                            return;
                        }
                        else if (Poc1IngestNoise.IsPresent)
                        {
                            // Read noise and remove relevant noise from whatIfOperationResult.
                            IngestNoise(whatIfOperationResult.Changes, Poc1NoiseStorageFile, Poc1UseDiffServer.IsPresent);
                        }
                    }
                    else if (Poc2WhatIf.IsPresent)
                    {
                        // Mark all resource parameters that are explictly defined in the template.
                        var marked = new Dictionary<string, bool>();
                        var resources = this.TemplateObject["resources"].ToJToken();

                        var whatIfOperationResult = whatIfResult.whatIfOperationResult;

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

                        // Remove noise in changes and update the whatIf result.
                        var updatedChanges = RemoveNoise(whatIfOperationResult.Changes, marked);
                        whatIfOperationResult.Changes = updatedChanges;
                    }
                    else if (Poc3WhatIf.IsPresent)
                    {
                        // Ensure only one step is being handeled per call. Poc3 requires 2 invocations of the cmdelt.
                        if (!(Poc3SaveNoise.IsPresent || Poc3IngestNoise.IsPresent) || (Poc3SaveNoise.IsPresent && Poc3IngestNoise.IsPresent))
                        {
                            throw new Exception("Must have only 1 of Poc1SaveNoise and Poc1IngestNoise set when running Poc3WhatIf.");
                        }
                        // Ensure storage file path is provided.
                        if (Poc1NoiseStorageFile == null || Poc1NoiseStorageFile == "")
                        {
                            throw new Exception("The Poc3MarkedPropertiesStorageFile parameter cannot be empty when running Poc3WhatIf.");
                        }

                        // Mark all resource parameters that are explictly defined in the template.
                        var marked = new Dictionary<string, bool>();
                        var resources = this.TemplateObject["resources"].ToJToken();

                        var whatIfOperationResult = whatIfResult.whatIfOperationResult;

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

                        if (Poc3SaveNoise.IsPresent)
                        {
                            // Save properties to simulate saving on deployment.
                            SaveMarkedProperties(marked, Poc3MarkedPropertiesStorageFile);
                            this.WriteDebug("POC1: Saved Noise file!");
                            
                            return;
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
                }

                // -----------------------------------------------------------------------------------------------------------------------------------------------

                string whatIfFormattedOutput = WhatIfOperationResultFormatter.Format(whatIfResult);
                if (this.ShouldProcessGivenCurrentConfirmFlagAndPreference() &&
                    this.ShouldSkipConfirmationIfNoChange() &&
                    whatIfResult.Changes.All(x => x.ChangeType == ChangeType.NoChange || x.ChangeType == ChangeType.Ignore))
                {

                    var whatIfInformation = new HostInformationMessage { Message = whatIfFormattedOutput };
                    var tags = new[] { "PSHOST" };

                    this.WriteInformation(whatIfInformation, tags);
                    //this.ExecuteDeployment();

                    this.WriteObject(whatIfResult);
                    return;
                }

                string cursorUp = $"{(char)27}[1A";
                
                // Use \r to override the built-in "What if:" in output.
                whatIfMessage = $"\r        \r{Environment.NewLine}{whatIfFormattedOutput}{Environment.NewLine}";
                warningMessage = $"{Environment.NewLine}{Resources.ConfirmDeploymentMessage}";
                captionMessage = $"{cursorUp}{Color.Reset}{whatIfMessage}";

                this.WriteInformation(whatIfMessage);
            }

            if (this.ShouldProcess(whatIfMessage, warningMessage, captionMessage))
            {
                //this.ExecuteDeployment();
            }

            this.WriteObject(whatIfResult);
        }
        protected void ExecuteDeployment()
        {
            var parameters = BuildDeploymentParameters();

            if (!string.IsNullOrEmpty(parameters.DeploymentDebugLogLevel))
            {
                this.WriteWarning(Resources.WarnOnDeploymentDebugSetting);
            }

            if (parameters.ScopeType == DeploymentScopeType.ResourceGroup)
            {
                this.WriteObject(this.NewResourceManagerSdkClient.ExecuteResourceGroupDeployment(parameters));
            }
            else
            {
                this.WriteObject(this.NewResourceManagerSdkClient.ExecuteDeployment(parameters));
            }
        }

        protected bool ShouldExecuteWhatIf() =>
            this.ShouldProcessGivenCurrentWhatIfFlagAndPreference() ||
            this.ShouldProcessGivenCurrentConfirmFlagAndPreference();

        private bool ShouldProcessGivenCurrentWhatIfFlagAndPreference()
        {
            if (this.MyInvocation.BoundParameters.GetOrNull("WhatIf") is SwitchParameter whatIfFlag)
            {
                return whatIfFlag.IsPresent;
            }

            if (this.SessionState == null)
            {
                return false;
            }

            object whatIfPreference = this.SessionState.PSVariable.GetValue("WhatIfPreference");

            return whatIfPreference is SwitchParameter whatIfPreferenceFlag
                ? whatIfPreferenceFlag.IsPresent
                : (bool)whatIfPreference;
        }

        private bool ShouldProcessGivenCurrentConfirmFlagAndPreference()
        {
            if (this.MyInvocation.BoundParameters.GetOrNull("Confirm") is SwitchParameter confirmFlag)
            {
                return confirmFlag.IsPresent;
            }

            if (this.SessionState == null)
            {
                return false;
            }

            var confirmPreference = (ConfirmImpact)this.SessionState.PSVariable.GetValue("ConfirmPreference");

            return this.ConfirmImpact >= confirmPreference;
        }

        public override object GetDynamicParameters()
        {
            if (!string.IsNullOrEmpty(QueryString))
            {
                if (QueryString.Substring(0, 1) == "?")
                    protectedTemplateUri = TemplateUri + QueryString;
                else
                    protectedTemplateUri = TemplateUri + "?" + QueryString;
            }
            return base.GetDynamicParameters();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------ Temporary Code for POC1 ----------------------------------------------------------------------------

        [Parameter(Mandatory = false, HelpMessage = "Switch to enable POC1 noise reduction if whatif flag is also provided.")]
        public SwitchParameter Poc1WhatIf { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 1 of POC1 noise reduction, or the saving of the noise to an external file (db).")]
        public SwitchParameter Poc1SaveNoise { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 2 of POC1 noise reduction, or the loading of the noise to an external file (db) and canceling out of said noise.\"")]
        public SwitchParameter Poc1IngestNoise { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch for POC1 implementation to choose javascript server to handle json diffs.")]
        public SwitchParameter Poc1UseDiffServer { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "File to save noise to.")]
        public string Poc1NoiseStorageFile { get; set; }

        private void SaveNoise(IList<WhatIfChange> changes, string noiseStorageFile)
        {
            var noise = new Dictionary<string, Dictionary<string, WhatIfPropertyChange>>();
            foreach (var change in changes)
            {
                var deltaMap = new Dictionary<string, WhatIfPropertyChange>();

                // If change type resulted in no change (Ignored, NoChange, ect.)
                if (change.Delta == null || change.Delta.Count == 0)
                {
                    continue;
                }

                foreach (var delta in change.Delta)
                {
                    deltaMap[delta.Path] = delta;
                }
                noise[change.ResourceId] = deltaMap;
            }

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(noiseStorageFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, noise);
            }
        }

        private void IngestNoise(IList<WhatIfChange> changes, string noiseStorageFile, bool useDiffServer)
        {
            // Read in the noise.
            Dictionary<string, Dictionary<string, WhatIfPropertyChange>> noise;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(noiseStorageFile))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                noise = serializer.Deserialize<Dictionary<string, Dictionary<string, WhatIfPropertyChange>>>(reader);
            }

            // Initialize httpClient if using for array diff server.
            HttpClient httpClient = useDiffServer ? new HttpClient() : null;

            foreach (var change in changes)
            {
                // If change type resulted in no change (Ignored, NoChange, ect.)
                if (change.Delta == null || !noise.ContainsKey(change.ResourceId))
                {
                    continue;
                }

                var newDelta = new List<WhatIfPropertyChange>();

                foreach (var deltaEntry in change.Delta)
                {
                    bool isNoise = false;

                    // If function exists in After, skip.
                    if (Regex.IsMatch(deltaEntry.After.ToJson(), ".*\\[.*\\(.*\\).*\\]"))
                    {
                        // If an unevaluated function exists, no way to determine if noise.
                        break;
                    }

                    // Check if noise is possible for given delta on resource id.
                    if (noise[change.ResourceId].ContainsKey(deltaEntry.Path) &&
                        noise[change.ResourceId][deltaEntry.Path] != null)
                    {
                        if (deltaEntry.Children == null || deltaEntry.Children.Count == 0)
                        {
                            // Case 1: Primative Delta
                            if (JsonEqualPrimative(noise[change.ResourceId][deltaEntry.Path].ToJToken(), deltaEntry.ToJson()) == false)
                            {
                                isNoise = true;

                                var splitIndex = deltaEntry.Path.LastIndexOf(".");
                                var parentPath = deltaEntry.Path.Substring(0, splitIndex);
                                var noisyProperty = deltaEntry.Path.Substring(splitIndex + 1);

                                if (deltaEntry.PropertyChangeType == PropertyChangeType.Modify)
                                {
                                    ((JObject)((JObject)change.After).SelectToken(parentPath))[noisyProperty] = deltaEntry.Before.ToJToken();
                                }
                                else if (deltaEntry.PropertyChangeType == PropertyChangeType.Delete)
                                {
                                    ((JObject)((JObject)change.After).SelectToken(parentPath)).Add(noisyProperty, deltaEntry.Before.ToJToken());
                                }
                                else if (deltaEntry.PropertyChangeType == PropertyChangeType.Create)
                                {
                                    ((JObject)((JObject)change.After).SelectToken(parentPath)).Remove(noisyProperty);
                                }
                            }
                        }
                        else if (deltaEntry.PropertyChangeType == PropertyChangeType.Array)
                        {
                            // Case 2: Array Delta
                            // Note: A top level Object Delta will never exist. It will be broken down into primitives on deployments` side.
                            // There could still be nested objects within the array though that will have to be accounted for.
                            var noiseEntryJToken = noise[change.ResourceId][deltaEntry.Path].ToJToken();
                            var deltaEntryJToken = deltaEntry.ToJToken();

                            // Run diff on array in server if boolean is provided.
                            bool match;
                            if (useDiffServer)
                            {
                                match = JsonDiffArrayAsync(httpClient, deltaEntryJToken, noiseEntryJToken).GetAwaiter().GetResult();
                            }
                            else
                            {
                                match = JsonDiffArray(deltaEntryJToken, noiseEntryJToken);
                            }

                            if (match)
                            {
                                isNoise = true;

                                var splitIndex = deltaEntry.Path.LastIndexOf(".");
                                var parentPath = deltaEntry.Path.Substring(0, splitIndex);
                                var noisyProperty = deltaEntry.Path.Substring(splitIndex + 1);

                                // TODO: Handle Arrays better.
                                ((JObject)((JObject)change.After).SelectToken(parentPath))[noisyProperty] = deltaEntry.Before.ToJToken();
                            }
                        }
                        else
                        {
                            // No other property change types should be encountered as whatif breaks objects down into primatives.
                            throw new Exception("PropertyChangeType of " + deltaEntry.PropertyChangeType.ToString() + " not supported.");
                        }
                    }

                    if (!isNoise)
                    {
                        // Valid changes get added added to new delta list.
                        newDelta.Add(deltaEntry);
                    }
                }

                if (newDelta.Count > 0)
                {
                    change.Delta = newDelta;
                }
                else
                {
                    // All deltas were removed as noise, so NoChange.
                    change.ChangeType = ChangeType.NoChange;
                    change.Delta = null;
                }
            }
        }

        private async Task<bool> JsonDiffArrayAsync(HttpClient httpClient, object result, object noise)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                left = result,
                right = noise
            }),
            Encoding.UTF8,
            "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("http://127.0.0.1:3000/diff", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse == "true" ? true : false;
        }

        private bool JsonEqualPrimative(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }

        private bool JsonDiffArray(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------ Temporary Code for POC2 ----------------------------------------------------------------------------

        [Parameter(Mandatory = false, HelpMessage = "Switch to enable POC2 noise reduction if whatif flag is also provided.")]
        public SwitchParameter Poc2WhatIf { get; set; }

        private string HandleFunctionInName(string resourceName)
        {
            // Process template parameters that are strings for resource name replacemaent.
            var templateParameters = this.GetTemplateParameterObject();

            resourceName = resourceName.Split('[')[1].Split(']')[0];

            while (resourceName.Contains("parameters("))
            {
                var parameterName = resourceName.Replace("parameters(", "*").Split('*')[1].Split(')')[0].Replace("'", "");
                var parameterValue = ((Hashtable)templateParameters[parameterName])["value"];
                resourceName = resourceName.Replace("parameters('" + parameterName + "')", (string)parameterValue);
            }

            // Handle resource name with format.
            if (resourceName.Contains("format("))
            {
                // TODO: Better handling of format (like allowing more than two arguments.
                var formatParameters = resourceName.Replace("format(", "*").Split('*')[1].Split(')')[0].Replace("'", "").Split(',');
                resourceName = string.Format(formatParameters[0], formatParameters[1].TrimStart(' '), formatParameters[2].TrimStart(' ')).Split('/').Last();
            }

            return resourceName;
        }


        private void MarkProperties(JToken currentToken, string resourceName, IDictionary<string, bool> marked)
        {
            // Need to handle arrays better.....
            if (currentToken.IsLeaf() || currentToken is JArray)
            {
                marked.Add(resourceName + "." + currentToken.Path.Substring(4), true);
                return;
            }

            foreach (var childToken in currentToken)
            {
                var pathSplit = childToken.Path.ToString().Split('.');
                var childTokenName = pathSplit[pathSplit.Length - 1];
                MarkProperties(childToken, resourceName, marked);
            }
        }

        private List<WhatIfChange> RemoveNoise(IList<WhatIfChange> changes, IDictionary<string, bool> marked)
        {
            var updatedChanges = new List<WhatIfChange>();

            foreach (var change in changes)
            {
                // Grab resource name from id.
                var changeResourceName = change.ResourceId.Split('/').Last();

                // Handle no change resources. For this POC, remove a NoChange resource if the resource was not part of the deployment.
                // Otherwise, keep NoChange, which the assumption that it only considers properties explictly set on resource.
                if (change.Delta == null || change.Delta.Count == 0)
                {
                    if (marked.ContainsKey(changeResourceName))
                    {
                        updatedChanges.Add(change);
                    }

                    continue;
                }

                var newDelta = new List<WhatIfPropertyChange>();
                foreach (var delta in change.Delta)
                {
                    if (marked.ContainsKey(changeResourceName + "." + delta.Path))
                    {
                        newDelta.Add(delta);
                    }
                    else
                    {
                        // Remove delta changes from change before and after.
                        var splitIndex = delta.Path.LastIndexOf(".");
                        var parentPath = delta.Path.Substring(0, splitIndex);
                        var noisyProperty = delta.Path.Substring(splitIndex + 1);

                        if (delta.PropertyChangeType == PropertyChangeType.Modify || delta.PropertyChangeType == PropertyChangeType.NoEffect || delta.PropertyChangeType == PropertyChangeType.Array)
                        {

                            ((JObject)((JObject)change.After).SelectToken(parentPath)).Remove(noisyProperty);
                            ((JObject)((JObject)change.After).SelectToken(parentPath)).Remove(noisyProperty);
                        }
                        else if (delta.PropertyChangeType == PropertyChangeType.Delete)
                        {
                            ((JObject)((JObject)change.Before).SelectToken(parentPath)).Remove(noisyProperty);
                        }
                        else if (delta.PropertyChangeType == PropertyChangeType.Create)
                        {
                            ((JObject)((JObject)change.After).SelectToken(parentPath)).Remove(noisyProperty);
                        }
                    }
                }

                if (newDelta.Count > 0)
                {
                    change.Delta = newDelta;
                }
                else
                {
                    change.Delta = null;
                    change.ChangeType = ChangeType.NoChange;
                }

                updatedChanges.Add(change);
            }

            return updatedChanges;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------ Temporary Code for POC3 ----------------------------------------------------------------------------

        [Parameter(Mandatory = false, HelpMessage = "File to write the map of marked properties to that will be used for subsequent deployments.")]
        public string Poc3MarkedPropertiesStorageFile { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to enable POC1 noise reduction if whatif flag is also provided.")]
        public SwitchParameter Poc3WhatIf { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 1 of POC1 noise reduction, or the saving of the noise to an external file (db).")]
        public SwitchParameter Poc3SaveNoise { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Switch to run step 2 of POC1 noise reduction, or the loading of the noise to an external file (db) and canceling out of said noise.\"")]
        public SwitchParameter Poc3IngestNoise { get; set; }

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
            if (markedPropertiesStorageFile == null)
            {
                return;
            }

            // Read in the old properties

            // add them to the map

            var previousMarkedProperties = new Dictionary<string, bool>();
        }
    }
}