---
external help file:
Module Name: DeploymentStacks
online version: https://learn.microsoft.com/powershell/module/deploymentstacks/new-deploymentstack
schema: 2.0.0
---

# New-DeploymentStack

## SYNOPSIS
Creates or updates a Deployment Stack.

## SYNTAX

### CreateExpanded1 (Default)
```
New-DeploymentStack -DeploymentStackName <String> -SubscriptionId <String>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### Create
```
New-DeploymentStack -DeploymentStackName <String> -ResourceGroupName <String> -SubscriptionId <String>
 -DeploymentStack <IDeploymentStack> [-AsJob] [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### Create1
```
New-DeploymentStack -DeploymentStackName <String> -SubscriptionId <String> -DeploymentStack <IDeploymentStack>
 [-AsJob] [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### Create2
```
New-DeploymentStack -DeploymentStackName <String> -ManagementGroupId <String>
 -DeploymentStack <IDeploymentStack> [-AsJob] [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### CreateExpanded
```
New-DeploymentStack -DeploymentStackName <String> -ResourceGroupName <String> -SubscriptionId <String>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### CreateExpanded2
```
New-DeploymentStack -DeploymentStackName <String> -ManagementGroupId <String>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### CreateViaIdentity
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity> -DeploymentStack <IDeploymentStack> [-AsJob]
 [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### CreateViaIdentity1
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity> -DeploymentStack <IDeploymentStack> [-AsJob]
 [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### CreateViaIdentity2
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity> -DeploymentStack <IDeploymentStack> [-AsJob]
 [-NoWait] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### CreateViaIdentityExpanded
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### CreateViaIdentityExpanded1
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### CreateViaIdentityExpanded2
```
New-DeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum>]
 [-ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum>] [-DebugSettingDetailLevel <String>]
 [-DenySettingApplyToChildScopes] [-DenySettingExcludedActions <String[]>]
 [-DenySettingExcludedPrincipals <String[]>] [-DenySettingMode <DenySettingsMode>] [-DeploymentScope <String>]
 [-Description <String>] [-Location <String>] [-ParameterLinkContentVersion <String>]
 [-ParameterLinkUri <String>] [-Parameters <IAny>] [-Tags <Hashtable>] [-Template <IAny>]
 [-TemplateLinkContentVersion <String>] [-TemplateLinkId <String>] [-TemplateLinkQueryString <String>]
 [-TemplateLinkRelativePath <String>] [-TemplateLinkUri <String>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

## DESCRIPTION
Creates or updates a Deployment Stack.

## EXAMPLES

### Example 1: {{ Add title here }}
```powershell
{{ Add code here }}
```

```output
{{ Add output here }}
```

{{ Add description here }}

### Example 2: {{ Add title here }}
```powershell
{{ Add code here }}
```

```output
{{ Add output here }}
```

{{ Add description here }}

## PARAMETERS

### -ActionOnUnmanageManagementGroups
Specifies the action that should be taken on the resource when the deployment stack is deleted.
Delete will attempt to delete the resource from Azure.
Detach will leave the resource in it's current state.

```yaml
Type: Sample.API.Support.DeploymentStacksDeleteDetachEnum
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ActionOnUnmanageResourceGroups
Specifies the action that should be taken on the resource when the deployment stack is deleted.
Delete will attempt to delete the resource from Azure.
Detach will leave the resource in it's current state.

```yaml
Type: Sample.API.Support.DeploymentStacksDeleteDetachEnum
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ActionOnUnmanageResources
Specifies the action that should be taken on the resource when the deployment stack is deleted.
Delete will attempt to delete the resource from Azure.
Detach will leave the resource in it's current state.

```yaml
Type: Sample.API.Support.DeploymentStacksDeleteDetachEnum
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsJob
Run the command as a job

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DebugSettingDetailLevel
Specifies the type of information to log for debugging.
The permitted values are none, requestContent, responseContent, or both requestContent and responseContent separated by a comma.
The default is none.
When setting this value, carefully consider the type of information that is being passed in during deployment.
By logging information about the request or response, sensitive data that is retrieved through the deployment operations could potentially be exposed.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingApplyToChildScopes
DenySettings will be applied to child scopes.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingExcludedActions
List of role-based management operations that are excluded from the denySettings.
Up to 200 actions are permitted.
If the denySetting mode is set to 'denyWriteAndDelete', then the following actions are automatically appended to 'excludedActions': '*/read' and 'Microsoft.Authorization/locks/delete'.
If the denySetting mode is set to 'denyDelete', then the following actions are automatically appended to 'excludedActions': 'Microsoft.Authorization/locks/delete'.
Duplicate actions will be removed.

```yaml
Type: System.String[]
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingExcludedPrincipals
List of AAD principal IDs excluded from the lock.
Up to 5 principals are permitted.

```yaml
Type: System.String[]
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingMode
denySettings Mode.

```yaml
Type: Sample.API.Support.DenySettingsMode
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeploymentScope
The scope at which the initial deployment should be created.
If a scope is not specified, it will default to the scope of the deployment stack.
Valid scopes are: management group (format: '/providers/Microsoft.Management/managementGroups/{managementGroupId}'), subscription (format: '/subscriptions/{subscriptionId}'), resource group (format: '/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}').

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeploymentStack
Deployment stack object.
To construct, see NOTES section for DEPLOYMENTSTACK properties and create a hash table.

```yaml
Type: Sample.API.Models.IDeploymentStack
Parameter Sets: Create, Create1, Create2, CreateViaIdentity, CreateViaIdentity1, CreateViaIdentity2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DeploymentStackName
Name of the deployment stack.

```yaml
Type: System.String
Parameter Sets: Create, Create1, Create2, CreateExpanded, CreateExpanded1, CreateExpanded2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Deployment stack description.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Identity Parameter
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Sample.API.Models.IDeploymentStacksIdentity
Parameter Sets: CreateViaIdentity, CreateViaIdentity1, CreateViaIdentity2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Location
The location of the deployment stack.
It cannot be changed after creation.
It must be one of the supported Azure locations.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ManagementGroupId
Management Group.

```yaml
Type: System.String
Parameter Sets: Create2, CreateExpanded2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
Run the command asynchronously

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ParameterLinkContentVersion
If included, must match the ContentVersion in the template.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ParameterLinkUri
The URI of the parameters file.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parameters
Name and value pairs that define the deployment parameters for the template.
Use this element when providing the parameter values directly in the request, rather than linking to an existing parameter file.
Use either the parametersLink property or the parameters property, but not both.
It can be a JObject or a well formed JSON string.

```yaml
Type: Sample.API.Models.IAny
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group.
The name is case insensitive.

```yaml
Type: System.String
Parameter Sets: Create, CreateExpanded
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscriptionId
The ID of the target subscription.

```yaml
Type: System.String
Parameter Sets: Create, Create1, CreateExpanded, CreateExpanded1
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tags
Deployment stack resource tags.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
The template content.
You use this element when you want to pass the template syntax directly in the request rather than link to an existing template.
It can be a JObject or well-formed JSON string.
Use either the templateLink property or the template property, but not both.

```yaml
Type: Sample.API.Models.IAny
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateLinkContentVersion
If included, must match the ContentVersion in the template.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateLinkId
The resource id of a Template Spec.
Use either the id or uri property, but not both.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateLinkQueryString
The query string (for example, a SAS token) to be used with the templateLink URI.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateLinkRelativePath
The relativePath property can be used to deploy a linked template at a location relative to the parent.
If the parent template was linked with a TemplateSpec, this will reference an artifact in the TemplateSpec.
If the parent was linked with a URI, the child deployment will be a combination of the parent and relativePath URIs

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateLinkUri
The URI of the template to deploy.
Use either the uri or id property, but not both.

```yaml
Type: System.String
Parameter Sets: CreateExpanded, CreateExpanded1, CreateExpanded2, CreateViaIdentityExpanded, CreateViaIdentityExpanded1, CreateViaIdentityExpanded2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Sample.API.Models.IDeploymentStack

### Sample.API.Models.IDeploymentStacksIdentity

## OUTPUTS

### Sample.API.Models.IDeploymentStack

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


`DEPLOYMENTSTACK <IDeploymentStack>`: Deployment stack object.
  - `[SystemDataCreatedAt <DateTime?>]`: The timestamp of resource creation (UTC).
  - `[SystemDataCreatedBy <String>]`: The identity that created the resource.
  - `[SystemDataCreatedByType <CreatedByType?>]`: The type of identity that created the resource.
  - `[SystemDataLastModifiedAt <DateTime?>]`: The timestamp of resource last modification (UTC)
  - `[SystemDataLastModifiedBy <String>]`: The identity that last modified the resource.
  - `[SystemDataLastModifiedByType <CreatedByType?>]`: The type of identity that last modified the resource.
  - `[ActionOnUnmanageManagementGroups <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[ActionOnUnmanageResourceGroups <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[ActionOnUnmanageResources <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[DebugSettingDetailLevel <String>]`: Specifies the type of information to log for debugging. The permitted values are none, requestContent, responseContent, or both requestContent and responseContent separated by a comma. The default is none. When setting this value, carefully consider the type of information that is being passed in during deployment. By logging information about the request or response, sensitive data that is retrieved through the deployment operations could potentially be exposed.
  - `[DenySettingApplyToChildScopes <Boolean?>]`: DenySettings will be applied to child scopes.
  - `[DenySettingExcludedActions <String[]>]`: List of role-based management operations that are excluded from the denySettings. Up to 200 actions are permitted. If the denySetting mode is set to 'denyWriteAndDelete', then the following actions are automatically appended to 'excludedActions': '*/read' and 'Microsoft.Authorization/locks/delete'. If the denySetting mode is set to 'denyDelete', then the following actions are automatically appended to 'excludedActions': 'Microsoft.Authorization/locks/delete'. Duplicate actions will be removed.
  - `[DenySettingExcludedPrincipals <String[]>]`: List of AAD principal IDs excluded from the lock. Up to 5 principals are permitted.
  - `[DenySettingMode <DenySettingsMode?>]`: denySettings Mode.
  - `[DeploymentScope <String>]`: The scope at which the initial deployment should be created. If a scope is not specified, it will default to the scope of the deployment stack. Valid scopes are: management group (format: '/providers/Microsoft.Management/managementGroups/{managementGroupId}'), subscription (format: '/subscriptions/{subscriptionId}'), resource group (format: '/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}').
  - `[Description <String>]`: Deployment stack description.
  - `[Location <String>]`: The location of the deployment stack. It cannot be changed after creation. It must be one of the supported Azure locations.
  - `[ParameterLinkContentVersion <String>]`: If included, must match the ContentVersion in the template.
  - `[ParameterLinkUri <String>]`: The URI of the parameters file.
  - `[Parameters <IAny>]`: Name and value pairs that define the deployment parameters for the template. Use this element when providing the parameter values directly in the request, rather than linking to an existing parameter file. Use either the parametersLink property or the parameters property, but not both. It can be a JObject or a well formed JSON string.
  - `[Tags <IDeploymentStackTags>]`: Deployment stack resource tags.
    - `[(Any) <String>]`: This indicates any property can be added to this object.
  - `[Template <IAny>]`: The template content. You use this element when you want to pass the template syntax directly in the request rather than link to an existing template. It can be a JObject or well-formed JSON string. Use either the templateLink property or the template property, but not both.
  - `[TemplateLinkContentVersion <String>]`: If included, must match the ContentVersion in the template.
  - `[TemplateLinkId <String>]`: The resource id of a Template Spec. Use either the id or uri property, but not both.
  - `[TemplateLinkQueryString <String>]`: The query string (for example, a SAS token) to be used with the templateLink URI.
  - `[TemplateLinkRelativePath <String>]`: The relativePath property can be used to deploy a linked template at a location relative to the parent. If the parent template was linked with a TemplateSpec, this will reference an artifact in the TemplateSpec.  If the parent was linked with a URI, the child deployment will be a combination of the parent and relativePath URIs
  - `[TemplateLinkUri <String>]`: The URI of the template to deploy. Use either the uri or id property, but not both.

`INPUTOBJECT <IDeploymentStacksIdentity>`: Identity Parameter
  - `[DeploymentStackName <String>]`: Name of the deployment stack.
  - `[ManagementGroupId <String>]`: Management Group.
  - `[ResourceGroupName <String>]`: The name of the resource group. The name is case insensitive.
  - `[SubscriptionId <String>]`: The ID of the target subscription.

## RELATED LINKS

