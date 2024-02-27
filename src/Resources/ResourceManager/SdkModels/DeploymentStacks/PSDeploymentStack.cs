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

using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkExtensions;
using Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.DeploymentStacks;
using Microsoft.Azure.Management.Resources.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectResources = Microsoft.Azure.Commands.ResourceManager.Cmdlets.Properties.Resources;

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels
{
    public class PSDeploymentStack
    {

        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public PSActionOnUnmanage actionOnUnmanage { get; set; }

        public SystemData systemData { get; set; }

        public string location { get; set; }

        public DenySettings denySettings { get; set; }

        public Dictionary<string, DeploymentVariable> outputs { get; set; }

        public IDictionary<string, PSDeploymentParameter> parameters { get; set; }

        public IDictionary<string, string> tags { get; set; }

        public DeploymentStacksParametersLink parametersLink { get; set; }

        public DeploymentStacksDebugSetting debugSetting { get; set; }

        public string provisioningState { get; set; }

        public string deploymentScope { get; set; }

        public string description { get; set; }

        public string duration { get; set; }

        public IList<ManagedResourceReference> resources { get; set; }

        public IList<ResourceReference> detachedResources { get; set; }

        public IList<ResourceReferenceExtended> failedResources { get; set; }

        public IList<ResourceReference> deletedResources { get; set; }

        public string deploymentId { get; set; }

        public ErrorResponse error { get; set; }

        public string correlationId { get; set; }

        public PSDeploymentStack() { }

        internal PSDeploymentStack(DeploymentStack deploymentStack)
        {
            this.id = deploymentStack.Id;
            this.name = deploymentStack.Name;
            this.type = deploymentStack.Type;
            this.systemData = deploymentStack.SystemData;
            this.actionOnUnmanage = ConvertActionOnUnmanage(deploymentStack.ActionOnUnmanage);
            this.location = deploymentStack.Location;
            this.parametersLink = deploymentStack.ParametersLink;
            this.debugSetting = deploymentStack.DebugSetting;
            this.provisioningState = deploymentStack.ProvisioningState;
            this.deploymentScope = deploymentStack.DeploymentScope;
            this.description = deploymentStack.Description;
            this.resources = deploymentStack.Resources;
            this.denySettings = deploymentStack.DenySettings;
            this.detachedResources = deploymentStack.DetachedResources;
            this.deletedResources = deploymentStack.DeletedResources;
            this.failedResources = deploymentStack.FailedResources;
            this.deploymentId = deploymentStack.DeploymentId;
            this.duration = deploymentStack.Duration;
            this.error = deploymentStack.Error;
            this.parametersLink = deploymentStack.ParametersLink;
            this.tags = deploymentStack.Tags;
            this.correlationId = deploymentStack.CorrelationId;


            // Convert the raw outputs and parameters objects to dictionaries.
            // TODO: Fix parameter output before introducing back
            //this.parameters = deploymentStack.Parameters != null ? FormatParametersObject(deploymentStack.Parameters) : null;
            this.outputs = deploymentStack.Outputs != null ? FormatOutputsObject(deploymentStack.Outputs) : null;
        }

        public PSActionOnUnmanage ConvertActionOnUnmanage(DeploymentStackPropertiesActionOnUnmanage actionOnUnmanage)
        {
            if (actionOnUnmanage.Resources == "detach" && actionOnUnmanage.ResourceGroups == "detach" && actionOnUnmanage.ManagementGroups == "detach")
            {
                return PSActionOnUnmanage.DetachAll;
            }

            if (actionOnUnmanage.Resources == "delete")
            {
                if (actionOnUnmanage.ResourceGroups == "delete" && actionOnUnmanage.ManagementGroups == "delete")
                {
                    return PSActionOnUnmanage.DeleteAll;
                }
                else if (actionOnUnmanage.ResourceGroups == "detach" && actionOnUnmanage.ManagementGroups == "detach")
                {
                    return PSActionOnUnmanage.DeleteResources;
                }
            }
          
            // TODO: Make better error.
            throw new Exception("Bad action on unmanage");     
        }

        public string GetFormattedParameterTable() 
        {
            // TODO: Need custom implementation for extracting parameter or keyvault reference info
            return "Will Implement";
        }

        public string GetFormattedOutputTable()
        {
            return ResourcesExtensions.ConstructDeploymentVariableTable(this.outputs);
        }

        public string GetFormattedTagTable()
        {
            return ResourcesExtensions.ConstructTagsTableFromIDictionary(this.tags);
        }

        private const char Whitespace = ' ';

        public string GetFormattedErrorString()
        {
            return this.error == null
                ? string.Empty
                : GetFormattedErrorString(this.error).TrimEnd('\r', '\n');
        }

        private static string GetFormattedErrorString(ErrorResponse error, int level = 0)
        {
            if (error.Details == null)
            {
                return string.Format(ProjectResources.DeploymentOperationErrorMessageNoDetails, error.Message, error.Code);
            }

            string errorDetail = null;

            foreach (ErrorResponse detail in error.Details)
            {
                errorDetail += GetIndentation(level) + GetFormattedErrorString(detail, level + 1) + System.Environment.NewLine;
            }

            return string.Format(ProjectResources.DeploymentOperationErrorMessage, error.Message, error.Code, errorDetail);
        }

        private static string GetIndentation(int l)
        {
            return new StringBuilder().Append(Whitespace, l * 2).Append(" - ").ToString();
        }

        internal static PSDeploymentStack FromAzureSDKDeploymentStack(DeploymentStack stack)
        {
            return stack != null
                ? new PSDeploymentStack(stack)
                : null;
        }

        internal static Dictionary<string, DeploymentVariable> FormatOutputsObject(object outputs)
        {
            var outputsPS = new Dictionary<string, DeploymentVariable>();

            // Extract DeploymentVariables from the passed in json object.
            var jObject = JObject.Parse(outputs.ToString());
            foreach (var props in jObject.Properties())
            {
                outputsPS[props.Name] = ExtractDeploymentVariableFromObject(props.Value as JObject);
            }

            // Continue deserialize if the type of Value in DeploymentVariable is array.
            outputsPS?.Values.ForEach(dv =>
            { 
                if ("Array".Equals(dv?.Type) && dv?.Value != null)
                {
                    dv.Value = dv.Value.ToString().FromJson<object[]>();
                }
            });

            return outputsPS;
        }

  /*      internal static Dictionary<string, PSDeploymentParameter> FormatParametersObject(IDictionary<string, DeploymentParameter> parameters)
        {
            var parametersPS = new Dictionary<string, PSDeploymentParameter>();

            foreach (var key in parameters.Keys)
            {
                if (parameters[key].Reference != null)
                {
                    parametersPS.Add(key, new PSDeploymentParameter { keyVaultReference = parameters[key].Reference });
                }
                else
                {
                    parametersPS.Add(key, new PSDeploymentParameter { parameter = ExtractDeploymentVariableFromDeploymentParameter(parameters[key]) });
                    var parameterType = parametersPS[key].parameter?.Type;
                    if (parametersPS[key].parameter?.Value != null && "Array".Equals(parameterType))
                    {
                        parametersPS[key].parameter.Value = JsonConvert.DeserializeObject<object[]>(parametersPS[key].parameter.Value.ToString());
                    }
                }
            }

            return parametersPS;
        }*/

        internal static DeploymentVariable ExtractDeploymentVariableFromObject(JObject jObject)
        {
            // Attempt to desialize the DeploymentVariable.
            var dVar = jObject.ToString().FromJson<DeploymentVariable>();

            // If the type is null, attempt to infer the type.
            if (dVar.Type is null)
            {
                JToken value; 
                var hasValue = jObject.TryGetValue("value", out value);
                
                // Value was there, we can infer the type.
                if (hasValue)
                {
                    // interpret type and find matching ARM type
                    switch (value.Type)
                    {
                        case JTokenType.String:
                            dVar.Type = "string";
                            break;
                        case JTokenType.Array:
                            dVar.Type = "array";
                            break;
                        case JTokenType.Integer:
                            dVar.Type = "integer";
                            break;
                        case JTokenType.Boolean:
                            dVar.Type = "boolean";
                            break;
                        default:
                            dVar.Type = "object";
                            break;
                    }
                }
            }
            
            return dVar;
        }
    }
}