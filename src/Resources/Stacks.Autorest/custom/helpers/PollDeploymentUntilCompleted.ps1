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

function PollDeploymentUntilCompleted {
    [OutputType([bool])]
    [CmdletBinding(PositionalBinding)]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.DoNotExportAttribute()]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Polls deployment until completed')]
    param(
        [Parameter(Position = 0, Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]
        $Message
    )

    process {
        try {
            # prompt user for confirmation
            # if input not understood, retry
            # yes, no, suspend, and ? (help)
        }
        catch {
            throw
        }
    }
}