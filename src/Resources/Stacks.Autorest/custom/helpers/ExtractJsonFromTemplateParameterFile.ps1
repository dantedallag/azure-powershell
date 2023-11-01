# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

# TODO: This is simple and will probably need to be replaced.

function ExtractJsonFromTemplateParameterFile {
    [OutputType([hashtable])]
    [CmdletBinding(PositionalBinding)]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.DoNotExportAttribute()]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Extracts parameters as hashtable from template parameter file')]
    param(
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]
        $File
    )

    process {    
        $resolvedTemplateFilePath = (Resolve-Path $File).Path
        $fileType = (Get-Item $resolvedTemplateFilePath).Extension
        
        if ($fileType -eq ".bicepparam") {
            # TODO: implementation of the Bicep utility in PowerShell.
            throw "Error: not currently accepting bicep files."
        }
        
        # If the file type passed in is not supported:
        if ($fileType -ne ".json")
        {
            throw "Error: unsupported template parameter file type."
        }

        $templateParams = Get-Content -Raw $resolvedTemplateFilePath | ConvertFrom-Json -AsHashtable
        $templateParams
    }
}