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

function ExtractJsonFromTemplateFile {
    [OutputType([string])]
    [CmdletBinding(PositionalBinding)]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.DoNotExportAttribute()]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Extracts json string from template ot template parameter file')]
    param(
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]
        $File
    )

    process {    
        try {
            $resolvedTemplateFilePath = (Resolve-Path $File).Path
        } catch {
            # TODO: Throw a custom error for file not found
            throw "Error: file was not found"    
        }

        $fileType = (Get-Item $resolvedTemplateFilePath).Extension
        
        # If a bicep file was passed in, we need to compile it down to json:
        if ($fileType -eq ".bicep") {
            # TODO: Convert if a Bicep file, which will require implementation of the Bicep utility in PowerShell.
            throw "Error: not currently accepting bicep files."
        }

        if ($fileType -eq ".bicepparam") {
            # TODO: Convert if a Bicep file, which will require implementation of the Bicep utility in PowerShell.
            throw "Error: not currently accepting bicep files."
        }
        
        # If the file type passed in is not supported:
        if ($fileType -ne ".json")
        {
            # TODO: Throw a custom error for bad file extension
            throw "Error: Unsupported file type."
        }

        $template = Get-Content -Raw $resolvedTemplateFilePath | ConvertFrom-Json -AsHashtable
        $template
    }
}