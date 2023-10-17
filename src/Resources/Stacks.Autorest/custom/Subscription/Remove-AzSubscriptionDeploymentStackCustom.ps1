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

function Remove-AzSubscriptionDeploymentStackCustom {
    [OutputType([Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack])]
    [CmdletBinding(DefaultParameterSetName='RemoveByName', PositionalBinding=$false, SupportsShouldProcess, ConfirmImpact='High')]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Creates a new subscription scoped deployment stack.')]
    param(
        [Parameter(Mandatory, ValueFromPipelineByPropertyName, ParameterSetName = 'RemoveByName', HelpMessage = 'The name of the stack.')]
        [Alias('StackName')]
        [string]
        ${Name},

        [Parameter(Mandatory, ValueFromPipelineByPropertyName, ParameterSetName = "RemoveByResourceId", HelpMessage = "ResourceId of the stack to delete.")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack]
        ${ResourceId},

        [Parameter(Mandatory, ValueFromPipelineByPropertyName, ParameterSetName = "RemoveByStackObject", HelpMessage = "The stack PS object.")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack]
        ${InputObject},

        [Parameter(HelpMessage = 'Signal to delete unmanaged stack resources after upating stack.')]
        [switch] 
        ${DeleteResources},

        [Parameter(HelpMessage = 'Signal to delete unmanaged stack resource groups after updating stack.')]
        [switch] 
        ${DeleteResourceGroups},

        [Parameter(HelpMessage = 'Signal to delete both resources and resource groups after updating stack.')]
        [switch] 
        ${DeleteAll},

        [Parameter(HelpMessage = "Do not ask for confirmation when overwriting an existing stack.")]
        [switch]
        ${Force},

        [Parameter(HelpMessage = "If set, a boolean will be returned with value dependent on cmdlet success.")]
        [bool]
        ${PassThru},

        # ---------- Internal parameters ----------

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
        #[Parameter(HelpMessage = 'Run the command asynchronously.')]
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
        try{           
            # -------------------------------------------------- Extract Stack Name If Required ---------------------------------------------------
            if ($PSBoundParameters.ContainsKey("ResourceId")) { 
                $PSBoundParameters["DeploymentStackName"] = GetNameFromResourceId $PSBoundParameters["ResourceId"]
                $null = $PSBoundParameters.Remove("ResourceId")
            }
    
            if ($PSBoundParameters.ContainsKey("StackObject")) {
                $PSBoundParameters["DeploymentStackName"] = $PSBoundParameters["StackObject"].Name
                $null = $PSBoundParameters.Remove("StackObject")
            }

            # -------------------------------------------------- Populate ActionOnUnmange Fields --------------------------------------------------
            $resourcesCleanupAction = "detach"
            $resourceGroupsCleanupAction = "detach"

            $shouldDeleteResources = $PSBoundParameters.ContainsKey('DeleteResources') -or $PSBoundParameters.ContainsKey('DeleteAll')
            $shouldDeleteResourceGroups = $PSBoundParameters.ContainsKey('DeleteResourceGroups') -or $PSBoundParameters.ContainsKey('DeleteAll')

            $null = $PSBoundParameters.Remove('DeleteResources')
            $null = $PSBoundParameters.Remove('DeleteResourceGroups')
            $null = $PSBoundParameters.Remove('DeleteAll')
            
            if ($shouldDeleteResources) {
                $resourcesCleanupAction = "delete"
            }

            if ($shouldDeleteResourceGroups) {
                $resourceGroupsCleanupAction = "delete"
            }

            $PSBoundParameters["ActionOnUnmanageResource"] = $resourcesCleanupAction
            $PSBoundParameters["ActionOnUnmanageResourceGroup"] = $resourceGroupsCleanupAction
            # Always detach MG, as delete functionality is not implemented.
            $PSBoundParameters["ActionOnUnmanageManagementGroup"] = "detach"

            # -------------------------------------------------- Retrieve Existing Stack --------------------------------------------------
            # Question: What is the best way to propagate required parameters as well as -Debug (when needed) to cmdlets we are calling?      
            $getParameters = @{}
            $getParameters['Name'] = $PSBoundParameters['Name']
            if ($PSBoundParameters.ContainsKey("HttpPipelineAppend")) {
                $getParameters['HttpPipelineAppend'] = $PSBoundParameters['HttpPipelineAppend']
            }
            if ($PSBoundParameters.ContainsKey("HttpPipelinePrepend")) {
                $getParameters['HttpPipelinePrepend'] = $PSBoundParameters['HttpPipelinePrepend']            
            }
            if ($PSBoundParameters.ContainsKey("NoWait")) {
                $getParameters['NoWait'] = $PSBoundParameters['NoWait']
            }
            if ($PSBoundParameters.ContainsKey("Proxy")) {
                $getParameters['Proxy'] = $PSBoundParameters['Proxy']
            }
            if ($PSBoundParameters.ContainsKey("ProxyCredential")) {
                $getParameters['ProxyCredential'] = $PSBoundParameters['ProxyCredential']
            }
            if ($PSBoundParameters.ContainsKey("ProxyUseDefaultCredential")) {
                $getParameters['ProxyUseDefaultCredentials'] = $PSBoundParameters['ProxyyUseDefaultCredentials']
            }
            
            try {   
                $currentStack = Az.DeploymentStacks\Get-AzSubscriptionDeploymentStackCustom @getParameters
            } catch {
                # Question: Is there a better way to catch this?
                if ($_.Exception.Code -ne "ResourceNotFound") {
                    throw 
                }    
            }

            # -------------------------------------------------- Delete Stack --------------------------------------------------
            # TODO: Work on to how to say this so it works better with ShouldProcess prompt.
            $warningMessage = "Overwrite Subscription scoped DeploymentStack in current Subscription with the following actions:" 
            $warning = StackExistsWarning $warningMessage $shouldDeleteResources $shouldDeleteResourceGroups
            
            if ($PSBoundParameters.ContainsKey('Force') -or ($PsCmdlet.ShouldProcess($PSBoundParameters["Name"], $warning))) {
                Az.DeploymentStacks.internal\Remove-AzDeploymentStacksDeploymentStack @PSBoundParameters
            }
        }
        catch {
            throw
        }
    }
}