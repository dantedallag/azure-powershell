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

function New-AzSubscriptionDeploymentStackCustom {
    [OutputType([Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack])]
    [CmdletBinding(DefaultParameterSetName='ByTemplateFileWithNoParameters', PositionalBinding=$false, SupportsShouldProcess, ConfirmImpact='Medium')]
    [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Description('Creates a new subscription scoped deployment stack')]
    param(
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithNoParameters', HelpMessage = 'TemplateFile to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterFile', HelpMessage = 'TemplateFile to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterUri', HelpMessage = 'TemplateFile to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterObject', HelpMessage = 'TemplateFile to be used to create the stack.')]
        [string]
        ${TemplateFile},
    
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithNoParameters', HelpMessage = 'Location of the Template to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterFile', HelpMessage = 'Location of the Template to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterUri', HelpMessage = 'Location of the Template to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterObject', HelpMessage = 'Location of the Template to be used to create the stack.')]
        [string]
        ${TemplateUri},

        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithNoParameters', HelpMessage = 'ResourceId of the TemplateSpec to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterUri', HelpMessage = 'ResourceId of the TemplateSpec to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterFile', HelpMessage = 'ResourceId of the TemplateSpec to be used to create the stack.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterObject', HelpMessage = 'ResourceId of the TemplateSpec to be used to create the stack.')]
        [string]
        ${TemplateSpecId},

        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterFile', HelpMessage = 'Parameter file to use for the template.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterFile', HelpMessage = 'Parameter file to use for the template.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterUri', HelpMessage = 'Parameter file to use for the template.')]
        [string]
        ${TemplateParameterFile},

        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterUri', HelpMessage = 'Location of the Parameter file to use for the template.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterUri', HelpMessage = 'Location of the Parameter file to use for the template.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterUri', HelpMessage = 'Location of the Parameter file to use for the template.')]
        [string]
        ${TemplateParameterUri},

        [Parameter(Mandatory, ParameterSetName = 'ByTemplateFileWithParameterObject', HelpMessage = 'A hash table which represents the parameters.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateUriWithParameterObject', HelpMessage = 'A hash table which represents the parameters.')]
        [Parameter(Mandatory, ParameterSetName = 'ByTemplateSpecWithParameterObject', HelpMessage = 'A hash table which represents the parameters.')]
        [hashtable]
        ${TemplateParameterObject},
    
        [Parameter(Mandatory, ValueFromPipelineByPropertyName, HelpMessage = 'The name of the stack.')]
        [Alias('StackName')]
        [string]
        ${Name},

        [Parameter(ValueFromPipelineByPropertyName, HelpMessage = "Description for the stack.")]
        [string]
        ${Description},

        [Parameter(Mandatory, HelpMessage = 'The location the resource resides in.')]
        [string]
        ${Location},

        [Parameter(HelpMessage = 'Signal to delete unmanaged stack resources after upating stack.')]
        [switch] 
        ${DeleteResources},

        [Parameter(HelpMessage = 'Signal to delete unmanaged stack resource groups after updating stack.')]
        [switch] 
        ${DeleteResourceGroups},

        [Parameter(HelpMessage = 'Signal to delete both resources and resource groups after updating stack.')]
        [switch] 
        ${DeleteAll},

        [Parameter(Mandatory, HelpMessage = 'Mode for DenySettings. Possible values include: "denyDelete", "denyWriteAndDelete", and "none".')]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.PSDenySettingsMode] 
        ${DenySettingsMode},

        [Parameter(HelpMessage = "List of AAD principal IDs excluded from the lock. Up to 5 principals are permitted.")]
        [string[]]
        ${DenySettingsExcludedPrincipal},

        [Parameter(HelpMessage = "List of role-based management operations that are excluded from " +
            "the denySettings. Up to 200 actions are permitted.")]
        [string[]]
        ${DenySettingsExcludedAction},

        [Parameter(HelpMessage = "Apply to child scopes.")]
        [switch]
        ${DenySettingsApplyToChildScopes},

        [Parameter(HelpMessage = "The tags to put on the deployment.")]
        [System.Collections.Hashtable]
        ${Tag},

        [Parameter(HelpMessage = "The query string (for example, a SAS token) to be used with the TemplateUri parameter. Would be used in case of linked templates")]
        [string]
        ${QueryString},

        [Parameter(HelpMessage = "The ResourceGroup at which the deployment will be created. If none is specified, it will default to the " +
        "subscription level scope of the deployment stack.")]
        [string]
        ${DeploymentResourceGroupName},

        # TODO: Will be needed potentially in Dynamic Parameters
        # [Parameter(HelpMessage = "Skips the PowerShell dynamic parameter processing that checks if the provided template parameter contains all necessary parameters used by the template. " +
        # "This check would prompt the user to provide a value for the missing parameters, but providing the -SkipTemplateParameterPrompt will ignore this prompt and " +
        # "error out immediately if a parameter was found not to be bound in the template. For non-interactive scripts, -SkipTemplateParameterPrompt can be provided " +
        # "to provide a better error message in the case where not all required parameters are satisfied.")]
        # [switch]
        # ${SkipTemplateParameterPrompt},

        [Parameter(HelpMessage = "Do not ask for confirmation when overwriting an existing stack.")]
        [switch]
        ${Force},

        #[Parameter(DontShow)]
        #[Parameter(HelpMessage = 'Run the command as a job.')]
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Category('Runtime')]
        [switch]
        ${AsJob},

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
            # -------------------------------------------------- Resolve Template Data -------------------------------------------------- 
            if ($PSBoundParameters.ContainsKey("TemplateFile")) {       
                $template = ExtractJsonFromTemplateFile $PSBoundParameters["TemplateFile"]
                $PSBoundParameters.Add("Template", $template)
                $null = $PSBoundParameters.Remove('TemplateFile')

            } elseif ($PSBoundParameters.ContainsKey("TemplateUri")) {
                $templateUri = $PSBoundParameters["TemplateUri"]
                if ($PSBoundParameters.ContainsKey("QueryString"))
                {
                    $queryString = $PSBoundParameters["QueryString"]
                    if ($queryString.Substring(0, 1) -eq "?") {
                        $templateUri = $templateUri + $queryString
                    } else {
                        $templateUri = $templateUri + "?" + $queryString
                    }
                    $null = $PSBoundParameters.Remove("QueryString")
                }
                $PSBoundParameters["TemplateLinkUri"] = $templateUri
                $null = $PSBoundParameters.Remove("TemplateUri")

            } elseif ($PSBoundParameters.ContainsKey("TemplateSpecId")) {
                $PSBoundParameters["TemplateLinkId"] = $PSBoundParameters["TemplateSpecId"]
                $null = $PSBoundParameters.Remove("TemplateSpecId")

            } else {
                # TODO: Throw a better error, though I don't think this can happen with parameter set as they are
                throw "Error: no template provided."
            }
            
            # -------------------------------------------------- Resolve Template Parameter Data --------------------------------------------------
            if ($PSBoundParameters.ContainsKey("TemplateParameterFile")) {  
                $parameters = ExtractJsonFromTemplateFile $PSBoundParameters["TemplateParameterFile"]
                $PSBoundParameters["Parameters"] = $parameters
                $null = $PSBoundParameters.Remove("TemplateParameterFile")

            } elseif ($PSBoundParameters.ContainsKey("TemplateParameterObject")) {
                $PSBoundParameters["Parameters"] = $PSBoundParameters["TemplateParameterObject"]
                $null = $PSBoundParameters.Remove("TemplateParameterObject")

            } elseif ($PSBoundParameters.ContainsKey("TemplateParameterUri")) {
                $PSBoundParameters["ParameterLinkUri"] = $PSBoundParameters["TemplateParameterUri"]
                $null = $PSBoundParameters.Remove("TemplateParameterUri")
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
            # Always detach MG, as delete functionality is not implemented
            $PSBoundParameters["ActionOnUnmanageManagementGroup"] = "detach"

            # -------------------------------------------------- Populate Deny Setting Variables --------------------------------------------------
            $PSBoundParameters["DenySettingMode"] = $PSBoundParameters["DenySettingsMode"]
            $null = $PSBoundParameters.Remove("DenySettingsMode")

            if ($PSBoundParameters.ContainsKey("DenySettingsExcludedPrincipal")) {
                $PSBoundParameters["DenySettingExcludedPrincipal"] = $PSBoundParameters["DenySettingsExcludedPrincipal"]
                $null = $PSBoundParameters.Remove("DenySettingsExcludedPrincipal")
            }

            if ($PSBoundParameters.ContainsKey("DenySettingsExcludedAction")) {
                $PSBoundParameters["DenySettingExcludedAction"] = $PSBoundParameters["DenySettingsExcludedAction"]
                $null = $PSBoundParameters.Remove("DenySettingsExcludedAction")
            }

            if ($PSBoundParameters.ContainsKey("DenySettingsApplyToChildScopes")) {
                $PSBoundParameters["DenySettingApplyToChildScope"] = $PSBoundParameters["DenySettingsApplyToChildScopes"]
                $null = $PSBoundParameters.Remove("DenySettingsApplyToChildScopes")
            }

            # -------------------------------------------------- Populate Deployment Scope --------------------------------------------------
            if ($PSBoundParameters.ContainsKey("DeploymentResourceGroupName")) {
                $currentSub = (Get-AzContext).Subscription.Id
                $PSBoundParameters["DeploymentScope"] = "/subscription/" + $currentSub + "/resourceGroups/" + $PSBoundParameters["DeploymentResourceGroupName"]
                $null = $PSBoundParameters.Remove("DeploymentResourceGroupName")
            }

            # -------------------------------------------------- Extract Flags for Confirmation --------------------------------------------------
            # TODO: Will be needed potentially in Dynamic Parameters
            # $skipTemplateParameterPrompt = $false
            # if ($PSBoundParameters.ContainsKey('SkipTemplateParameterPrompt')) {
            #     $skipTemplateParameterPrompt = $true
            #     $null = $PSBoundParameters.Remove('SkipTemplateParameterPrompt')
            # }

            # -------------------------------------------------- Retrieve Existing Stack --------------------------------------------------
            $name = $PSBoundParameters['Name']
            try {   
                $currentStack = Az.DeploymentStacks\Get-AzSubscriptionDeploymentStackCustom $name
            } catch {
                # TODO: Should catch not found errors here
            }

            # -------------------------------------------------- Populate Tags From Existing Stack --------------------------------------------------
            # if (($null -ne $currentStack) -and ($false -eq $PSBoundParameters.ContainsKey("Tag"))) {
            #     # TODO: Not sure about types
            #     $PSBoundParameters["Tag"] = $currentStack.Tag
            # }

            # -------------------------------------------------- Upsert Stack --------------------------------------------------
            $force = $PSBoundParameters.ContainsKey('Force')
            $null = $PSBoundParameters.Remove("Force")

            if (($null -ne $currentStack) -or $force) {
                $warningMessage = "Are you sure you want to remove Subscription scoped DeploymentStack '{$name}' in current Subscription with the following actions?" 
                $warning = StackExistsWarning $warningMessage $PSBoundParameters.ContainsKey('DeleteResources') $PSBoundParameters.ContainsKey('DeleteResourceGroups')
                if (Confirmation $warning) {
                    # TODO: Inplement cofirmation logic, looping
                    Az.DeploymentStacks.internal\New-AzDeploymentStacksDeploymentStack @PSBoundParameters -Break
                } 
            } else {
                Az.DeploymentStacks.internal\New-AzDeploymentStacksDeploymentStack @PSBoundParameters -Break
            }

            # -------------------------------------------------- Polling For Completion --------------------------------------------------

            # TODO: Requires more code to be rewritten

            # -------------------------------------------------- Error Validation + Printing Error --------------------------------------------------

            $PSBoundParameters
        }
        catch {
            throw
        }
    }
}