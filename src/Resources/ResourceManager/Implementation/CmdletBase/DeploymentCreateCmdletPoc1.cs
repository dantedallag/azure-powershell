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
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Extensions;
    using Microsoft.Azure.Management.Resources.Models;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using System.Text.RegularExpressions;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using JsonDiffPatchDotNet;
    using Newtonsoft.Json;

    public abstract partial class DeploymentCreateCmdlet
    {
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

        private void Poc1(WhatIfOperationResult whatIfOperationResult)
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

            if (Poc1SaveNoise.IsPresent)
            {
                // Save noise to provided file.
                // NOTE: This is a simulation of something that would happen during a PUT. Only the ingesting of the noise
                // would happen during a whatif call.
                SaveNoise(whatIfOperationResult.Changes, Poc1NoiseStorageFile);
                this.WriteDebug("POC1: Saved Noise file!");
            }
            else if (Poc1IngestNoise.IsPresent)
            {
                // Read noise and remove relevant noise from whatIfOperationResult.
                IngestNoise(whatIfOperationResult.Changes, Poc1NoiseStorageFile, Poc1UseDiffServer.IsPresent);
            }
        }


        /// <summary>
        /// For each resource change recorded in the whatif result, write the delta of each property to the file.
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="noiseStorageFile"></param>
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

        /// <summary>
        /// Read the noise file into a map and compare values with newly returned whatif result.
        /// If the diffserver is specified, use the js server rather than the local json diff library.
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="noiseStorageFile"></param>
        /// <param name="useDiffServer"></param>
        /// <exception cref="Exception"></exception>
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

        private bool JsonEqualPrimative(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
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

        private bool JsonDiffArray(JToken result, JToken noise)
        {
            JsonDiffPatch patch = new JsonDiffPatch();

            return patch.Diff(result, noise) == null;
        }

    }
}
