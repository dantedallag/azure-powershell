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

function Get-AzSubscriptionDeploymentStackCustom {
    [OutputType([Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack])]
    [CmdletBinding(DefaultParameterSetName='ListDeploymentStacks', PositionalBinding)]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Retrieves a subscription scoped deployment stack')]
    param(
        [Alias("StackName")]
        [Parameter(Position = 0, Mandatory, ValueFromPipelineByPropertyName, ParameterSetName = "GetByNameParameterSetname", HelpMessage = "The name of the DeploymentStack to get.")]
        [ValidateNotNullOrEmpty()]
        [string]
        $Name,

        [Alias("Id")]
        [Parameter(Mandatory, ValueFromPipelineByPropertyName, ParameterSetName = "GetByResourceIdParameterSetName", HelpMessage = "ResourceId of the DeploymentStack to get.")]
        [ValidateNotNullOrEmpty()]
        [string]
        $ResourceId,

        [Parameter(DontShow)]
        [ValidateNotNull()]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Runtime.SendAsyncStep[]]
        # SendAsync Pipeline Steps to be appended to the front of the pipeline.
        ${HttpPipelineAppend},

        [Parameter(DontShow)]
        [ValidateNotNull()]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Runtime.SendAsyncStep[]]
        # SendAsync Pipeline Steps to be prepended to the front of the pipeline.
        ${HttpPipelinePrepend},

        [Parameter(DontShow)]
        # [Parameter(HelpMessage = 'Run the command asynchronously.')]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [switch]
        ${NoWait},

        [Parameter(DontShow)]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [System.Uri]
        # The URI for the proxy server to use.
        ${Proxy},

        [Parameter(DontShow)]
        [ValidateNotNull()]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [System.Management.Automation.PSCredential]
        # Credentials for a proxy server to use for the remote call.
        ${ProxyCredential},

        [Parameter(DontShow)]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [switch]
        # Use the default credentials for the proxy.
        ${ProxyUseDefaultCredentials}
    )

    process {
        if ($PSBoundParameters.ContainsKey("ResourceId")) { 
            $rname = GetNameFromResourceId $PSBoundParameters["ResourceId"]
            $null = $PSBoundParameters.Remove("ResourceId")
            $PSBoundParameters["Name"] = $rname
        }

        Az.DeploymentStacks.internal\Get-AzDeploymentStacksDeploymentStack @PSBoundParameters
    }
}