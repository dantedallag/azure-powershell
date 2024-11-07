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

    public abstract class DeploymentCreateCmdlet: DeploymentWhatIfCmdlet
    {
        [Parameter(Mandatory = false, HelpMessage = "The query string (for example, a SAS token) to be used with the TemplateUri parameter. Would be used in case of linked templates")]
        public string QueryString { get; set; }

        // Noise parameters for testing ----------------
        [Parameter(Mandatory = false)]
        public SwitchParameter SaveNoise { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter IngestNoise { get; set; }

        // Simulates the DB where noise will be kept.
        [Parameter(Mandatory = false)]
        public string NoiseStorageFile { get; set; }

        // File to write adjusted whatif result.
        [Parameter(Mandatory = false)]
        public string NoiseRemovalResultFile { get; set; }

        // File tp write full whatif result.
        [Parameter(Mandatory = false)]
        public string RawOutputFile { get; set; }

        // ---------------------------------------------

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

            if (this.ShouldExecuteWhatIf())
            {
                PSWhatIfOperationResult whatIfResult = this.ExecuteWhatIf();
                HttpClient httpClient = new HttpClient();

                // --------------------------------------------------------------------------------------------------------------------------------------------

                if (SaveNoise.IsPresent)
                {
                    if (RawOutputFile != null)
                    {
                        File.WriteAllText(RawOutputFile, whatIfResult.whatIfOperationResult.ToFormattedJson());
                    }

                    var noise = new Dictionary<string, Dictionary<string, WhatIfPropertyChange>>();
                    foreach (var change in whatIfResult.whatIfOperationResult.Changes)
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
                    using (StreamWriter sw = new StreamWriter(NoiseStorageFile))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, noise);
                    }
                }
                else if (IngestNoise.IsPresent)
                {
                    if (RawOutputFile != null)
                    {
                        File.WriteAllText(RawOutputFile, whatIfResult.whatIfOperationResult.ToFormattedJson());
                    }

                    Dictionary<string, Dictionary<string, WhatIfPropertyChange>> noise;
                    JsonSerializer serializer = new JsonSerializer();
                    using (StreamReader sr = new StreamReader(NoiseStorageFile))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        noise = serializer.Deserialize<Dictionary<string, Dictionary<string, WhatIfPropertyChange>>>(reader);
                    }
                    var newChanges = new List<WhatIfChange>();

                    foreach (var change in whatIfResult.whatIfOperationResult.Changes)
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

                            // TODO: May be a better regex to tell if a function exists in an after value:
                            if (Regex.IsMatch(deltaEntry.After.ToJson(), ".*\\[.*\\(.*\\).*\\]"))
                            {
                                // If an unevaluated function exists, no way to determine if noise.
                                break;
                            }

                            // Check if noise is possible for given delta on resource id.
                            if (noise[change.ResourceId].ContainsKey(deltaEntry.Path) &&
                                noise[change.ResourceId][deltaEntry.Path] != null)
                            {
                                if (deltaEntry.Children.Count == 0)
                                {
                                    // Primative Delta
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
                                    // (Case 2): Array Delta
                                    // Note: A top level Object Delta will never exist. It will be broken down into primitives on deployments` side.
                                    // There could still be nested objects within the array though that will have to be accounted for.
                                    var noiseEntryJToken = noise[change.ResourceId][deltaEntry.Path].ToJToken();
                                    var deltaEntryJToken = deltaEntry.ToJToken();

                                    //var match = JsonDiffAsync(httpClient, deltaEntryJToken, noiseEntryJToken).GetAwaiter().GetResult();
                                    var match = JsonEqualArray(deltaEntryJToken, noiseEntryJToken);

                                    if (match)
                                    {
                                        isNoise = true;

                                        var splitIndex = deltaEntry.Path.LastIndexOf(".");
                                        var parentPath = deltaEntry.Path.Substring(0, splitIndex);
                                        var noisyProperty = deltaEntry.Path.Substring(splitIndex + 1);

                                        ((JObject)((JObject)change.After).SelectToken(parentPath))[noisyProperty] = deltaEntry.Before.ToJToken();
                                    }
                                }
                            }

                            if (!isNoise)
                            {
                                newDelta.Add(deltaEntry);
                            }
                        }

                        if (newDelta.Count > 0)
                        {
                            change.Delta = newDelta;
                        }
                        else
                        {
                            change.ChangeType = ChangeType.NoChange;
                            change.Delta = null;
                        }
                    }

                    if (NoiseRemovalResultFile != null)
                    {
                        File.WriteAllText(NoiseRemovalResultFile, whatIfResult.whatIfOperationResult.ToFormattedJson());
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

                    return;
                }

                string cursorUp = $"{(char)27}[1A";

                // Use \r to override the built-in "What if:" in output.
                whatIfMessage = $"\r        \r{Environment.NewLine}{whatIfFormattedOutput}{Environment.NewLine}";
                warningMessage = $"{Environment.NewLine}{Resources.ConfirmDeploymentMessage}";
                captionMessage = $"{cursorUp}{Color.Reset}{whatIfMessage}";
            }

            if (this.ShouldProcess(whatIfMessage, warningMessage, captionMessage))
            {
                //this.ExecuteDeployment();
            }
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
                if(QueryString.Substring(0,1) == "?")
                    protectedTemplateUri = TemplateUri + QueryString;
                else
                    protectedTemplateUri = TemplateUri + "?" + QueryString;
            }
            return base.GetDynamicParameters();
        }

        //private async Task<bool> JsonDiffAsync(HttpClient httpClient, object result, object noise)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(new
        //    {
        //        left = result,
        //        right = noise
        //    }),
        //    Encoding.UTF8,
        //    "application/json");

        //    HttpResponseMessage response = await httpClient.PostAsync("http://127.0.0.1:3000/diff", content);

        //    var jsonResponse = await response.Content.ReadAsStringAsync();
        //    return jsonResponse == "true" ? true : false;
        //}

        private bool JsonEqualPrimative(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }

        private bool JsonEqualObject(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }

        private bool JsonEqualArray(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }
    }
}
