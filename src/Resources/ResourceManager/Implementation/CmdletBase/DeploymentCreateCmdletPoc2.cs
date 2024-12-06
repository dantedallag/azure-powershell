namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Implementation.CmdletBase
{
    using System.Linq;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
    using Microsoft.Azure.Management.Resources.Models;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using System.Collections;
    using Microsoft.Azure.Commands.Common.Authentication.Abstractions;

    public abstract partial class DeploymentCreateCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "Switch to enable POC2 noise reduction if whatif flag is also provided.")]
        public SwitchParameter Poc2WhatIf { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Provides more granular array noise handling in POC2 and POC3 as an experimental feature.")]
        public SwitchParameter Poc2And3BetterArrayHandling { get; set; }

        private void Poc2(WhatIfOperationResult whatIfOperationResult)
        {
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

            // Remove noise in changes and update the whatIf result.
            var updatedChanges = RemoveNoise(whatIfOperationResult.Changes, marked);
            whatIfOperationResult.Changes = updatedChanges;
        }

        private JToken ExtractResourcesFromTemplate()
        {
            if (!string.IsNullOrEmpty(TemplateFile))
            {
                return FileUtilities.DataStore.ReadFileAsStream(TemplateFile).FromJson<JObject>()["resources"];
            }
            else
            {
                return TemplateObject.ToJToken()["resources"];
            }

        }

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
            if (currentToken.IsLeaf())
            {
                var index = currentToken.Path.IndexOf(']');
                marked.Add(resourceName + currentToken.Path.Remove(0, index + 1), true);
                return;
            }

            foreach (var childToken in currentToken)
            {
                var pathSplit = childToken.Path.ToString().Split('.');
                var childTokenName = pathSplit[pathSplit.Length - 1];
                MarkProperties(childToken, resourceName, marked);
            }
        }

        private bool CheckForArrayValidChanges(WhatIfPropertyChange current, string path, IDictionary<string, bool> marked)
        {
            // Need to handle arrays better.....
            if (current.Children == null || current.Children.Count == 0)
            {
                if (marked.ContainsKey(path + "." + current.Path))
                {
                    return true;
                }
                else
                {
                    current.PropertyChangeType = PropertyChangeType.NoEffect;
                    return false;
                }
            }

            bool validDelta = false;
            
            foreach (var childDelta in current.Children)
            {
                string adjustedPath;

                if (int.TryParse(current.Path, out _))
                {
                    adjustedPath = path + "[" + current.Path + "]";
                }
                else
                {
                    adjustedPath = path + "." + current.Path;
                }

                bool changeDetected = CheckForArrayValidChanges(childDelta, adjustedPath, marked);

                if (changeDetected)
                {
                    validDelta = true;
                }
            }

            return validDelta;
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
                    if (delta.PropertyChangeType == PropertyChangeType.Array && Poc2And3BetterArrayHandling.IsPresent)
                    {
                        var shouldAddDelta = CheckForArrayValidChanges(delta, changeResourceName, marked);

                        if (shouldAddDelta)
                        {
                            newDelta.Add(delta);
                        }
                    }
                    else if (marked.ContainsKey(changeResourceName + "." + delta.Path))
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
    }
}
