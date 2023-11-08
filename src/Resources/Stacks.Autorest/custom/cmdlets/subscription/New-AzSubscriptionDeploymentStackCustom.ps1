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
    [OutputType([Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.DeploymentStack])]
    [CmdletBinding(DefaultParameterSetName = 'ByTemplateFileWithNoParameters', PositionalBinding = $false, SupportsShouldProcess, ConfirmImpact='High')]
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
        #NOTE: This is only completing and is not validating (we currently do both). We were advised to only use completers.
        [Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.PSArgumentCompleterAttribute('None', 'DenyDelete', 'DenyWriteAndDelete')]
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
        [hashtable]
        ${Tag},

        [Parameter(HelpMessage = "The query string (for example, a SAS token) to be used with the TemplateUri parameter. Would be used in case of linked templates")]
        [string]
        ${QueryString},

        [Parameter(HelpMessage = "The ResourceGroup at which the deployment will be created. If none is specified, it will default to the " +
        "subscription level scope of the deployment stack.")]
        [string]
        
        # TODO: Will need to create custom completer that will fetch resource groups in current subscription.
        ${DeploymentResourceGroupName},

        [Parameter(HelpMessage = "Do not ask for confirmation when overwriting an existing stack.")]
        [switch]
        ${Force},

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
        # -------------------------------------------------- Resolve Template Data --------------------------------------------------
        
        # TODO: Will have to support bicep. 
        # TODO: Template/parameter reading in will probably have to be smarter...I want to talk through some of these libraries that PS
        # team has been maintaining.

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
            throw "Error: A TemplateFile, TemplateUri, or TemplateSpecId must be provided."
        }
        
        # -------------------------------------------------- Resolve Template Parameter Data --------------------------------------------------

        # TODO: This C# needs to be emulated better.
        # Dictionary<string, object> parametersDictionary = parameters?.ToDictionary(false);
        # string parametersContent = parametersDictionary != null
        #     ? PSJsonSerializer.Serialize(parametersDictionary)
        #     : null;
        # deploymentStackModel.Parameters = !string.IsNullOrEmpty(parametersContent)
        #     ? JObject.Parse(parametersContent)
        #     : null;

        if ($PSBoundParameters.ContainsKey("TemplateParameterFile")) {  
            $parameters = ExtractJsonFromTemplateParameterFile $PSBoundParameters["TemplateParameterFile"]
            $PSBoundParameters["Parameter"] = $parameters
            $null = $PSBoundParameters.Remove("TemplateParameterFile")

        } elseif ($PSBoundParameters.ContainsKey("TemplateParameterObject")) {
            $PSBoundParameters["Parameter"] = $PSBoundParameters["TemplateParameterObject"]
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
        # Always detach MG, as delete functionality is not implemented.
        $PSBoundParameters["ActionOnUnmanageManagementGroup"] = "detach"

        # -------------------------------------------------- Populate Deployment Scope --------------------------------------------------
        if ($PSBoundParameters.ContainsKey("DeploymentResourceGroupName")) {
            $currentSub = (Get-AzContext).Subscription.Id
            $PSBoundParameters["DeploymentScope"] = "/subscription/" + $currentSub + "/resourceGroups/" + $PSBoundParameters["DeploymentResourceGroupName"]
            $null = $PSBoundParameters.Remove("DeploymentResourceGroupName")
        }

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
            # Question: Is there a better way to catch this? It seems like the exceptions coming back from the generated cmdlets are generic and 
            # so the only way I can see to handle this is string matching the message, which isn't great.
            if ($_.Exception.Message -notmatch "[ResourceNotFound]") {
                throw 
            }    
        }
        # -------------------------------------------------- Populate Tags From Existing Stack --------------------------------------------------
        
        # TODO: Is there a way to prevent Tags to be saved as an empty object? For lists it seems like we are saving empty lists and $null, but we are defaulting to an empty object for Tags.
        # Tags are special because we don't want to reset the Tags when we pass an empty object.
        if (($null -ne $currentStack) -and ($null -ne $currentStack.Tags) -and ($currentStack.Tags.Keys.Count -ne 0) -and ($false -eq $PSBoundParameters.ContainsKey("Tag"))) {
            $PSBoundParameters["Tag"] = ConvertTagsObjectToHashtable $currentStack.Tags
        }

        # -------------------------------------------------- Upsert Stack --------------------------------------------------
        # TODO: Work on to how to say this so it works better with ShouldProcess prompt.
        $warningMessage = "Overwrite Subscription scoped DeploymentStack in current Subscription with the following actions:" 
        $warning = StackExistsWarning $warningMessage $shouldDeleteResources $shouldDeleteResourceGroups
        
        if (($null -eq $currentStack) -or $PSBoundParameters.ContainsKey('Force') -or ($PsCmdlet.ShouldProcess($PSBoundParameters["Name"], $warning))) {
            try {
                Az.DeploymentStacks.internal\New-AzDeploymentStacksDeploymentStack @PSBoundParameters
            } catch {
                # TODO: Write a better exception parser to write error.
                $_.Exception.Message
                Az.DeploymentStacks\Get-AzSubscriptionDeploymentStackCustom @getParameters
            }
        }
    }

    # TODO: Assure exceptions/errors are being handled correctly.
}