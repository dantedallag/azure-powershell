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

function StackExistsWarning {
    [OutputType([string])]
    [CmdletBinding(PositionalBinding)]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.DoNotExportAttribute()]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Creates a warning for the stack existing when you try to overwrite it')]
    param(
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]
        $MainMessage,

        [Parameter(Position = 1, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [bool]
        $ShouldDeleteResources,

        [Parameter(Position = 2, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [bool]
        $ShouldDeleteResourceGroups
    )

    process {
        $message = $PSBoundParameters["MainMessage"]
        $shouldDeleteResources = $PSBoundParameters["ShouldDeleteResources"]
        $shouldDeleteResourceGroups = $PSBoundParameters["ShouldDeleteResourceGroups"]

        if ($shouldDeleteResources -or $shouldDeleteResourceGroups) {
            $message += "`nDeleting: "
        }

        if ($shouldDeleteResources) {
            $message += "resources"
        }

        if ($shouldDeleteResources -and $shouldDeleteResourceGroups) {
            $message += ", "
        }

        if ($shouldDeleteResourceGroups) {
            $message += "resourceGroups"
        }

        if (!$shouldDeleteResources -or !$shouldDeleteResourceGroups) {
            $message += "`nDetaching: "
        }

        if (!$shouldDeleteResources) {
            $message += "resources"
        }

        if (!$shouldDeleteResources -and !$shouldDeleteResourceGroups) {
            $message += ", "
        }

        if (!$shouldDeleteResourceGroups) {
            $message += "resourceGroups"
        }

        $message
    }
}