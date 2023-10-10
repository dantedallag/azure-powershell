---
external help file:
Module Name: Az.Resources
online version: https://learn.microsoft.com/powershell/module/az.resources/save-azsubscriptiondeploymentstackcustom
schema: 2.0.0
---

# Save-AzSubscriptionDeploymentStackCustom

## SYNOPSIS
Retrieves the template or template link of a subscription scoped deployment stack.

## SYNTAX

### SaveByName (Default)
```
Save-AzSubscriptionDeploymentStackCustom [-DeploymentStackName] <String> [<CommonParameters>]
```

### SaveByResourceId
```
Save-AzSubscriptionDeploymentStackCustom -ResourceId <String> [<CommonParameters>]
```

### SaveByStackObject
```
Save-AzSubscriptionDeploymentStackCustom -InputObject <DeploymentStack> [<CommonParameters>]
```

## DESCRIPTION
Retrieves the template or template link of a subscription scoped deployment stack.

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

### -DeploymentStackName
The name of the DeploymentStack to get template/template link for.

```yaml
Type: System.String
Parameter Sets: SaveByName
Aliases: StackName

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -InputObject
The stack PS object to get template/template link for.
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack
Parameter Sets: SaveByStackObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ResourceId
ResourceId of the DeploymentStack to get template/template link for.

```yaml
Type: System.String
Parameter Sets: SaveByResourceId
Aliases: Id

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack

### System.String

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStackTemplateDefinition

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


`INPUTOBJECT <DeploymentStack>`: The stack PS object to get template/template link for.
  - `[ActionOnUnmanageManagementGroup <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[ActionOnUnmanageResource <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[ActionOnUnmanageResourceGroup <DeploymentStacksDeleteDetachEnum?>]`: Specifies the action that should be taken on the resource when the deployment stack is deleted. Delete will attempt to delete the resource from Azure. Detach will leave the resource in it's current state.
  - `[DebugSettingDetailLevel <String>]`: Specifies the type of information to log for debugging. The permitted values are none, requestContent, responseContent, or both requestContent and responseContent separated by a comma. The default is none. When setting this value, carefully consider the type of information that is being passed in during deployment. By logging information about the request or response, sensitive data that is retrieved through the deployment operations could potentially be exposed.
  - `[DenySettingApplyToChildScope <Boolean?>]`: DenySettings will be applied to child scopes.
  - `[DenySettingExcludedAction <String[]>]`: List of role-based management operations that are excluded from the denySettings. Up to 200 actions are permitted. If the denySetting mode is set to 'denyWriteAndDelete', then the following actions are automatically appended to 'excludedActions': '*/read' and 'Microsoft.Authorization/locks/delete'. If the denySetting mode is set to 'denyDelete', then the following actions are automatically appended to 'excludedActions': 'Microsoft.Authorization/locks/delete'. Duplicate actions will be removed.
  - `[DenySettingExcludedPrincipal <String[]>]`: List of AAD principal IDs excluded from the lock. Up to 5 principals are permitted.
  - `[DenySettingMode <DenySettingsMode?>]`: denySettings Mode.
  - `[DeploymentScope <String>]`: The scope at which the initial deployment should be created. If a scope is not specified, it will default to the scope of the deployment stack. Valid scopes are: management group (format: '/providers/Microsoft.Management/managementGroups/{managementGroupId}'), subscription (format: '/subscriptions/{subscriptionId}'), resource group (format: '/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}').
  - `[Description <String>]`: Deployment stack description.
  - `[Location <String>]`: The location of the deployment stack. It cannot be changed after creation. It must be one of the supported Azure locations.
  - `[Parameter <IDeploymentStackPropertiesParameters>]`: Name and value pairs that define the deployment parameters for the template. Use this element when providing the parameter values directly in the request, rather than linking to an existing parameter file. Use either the parametersLink property or the parameters property, but not both. It can be a JObject or a well formed JSON string.
    - `[(Any) <Object>]`: This indicates any property can be added to this object.
  - `[ParameterLinkContentVersion <String>]`: If included, must match the ContentVersion in the template.
  - `[ParameterLinkUri <String>]`: The URI of the parameters file.
  - `[Tag <IDeploymentStackTags>]`: Deployment stack resource tags.
    - `[(Any) <String>]`: This indicates any property can be added to this object.
  - `[Template <IDeploymentStackPropertiesTemplate>]`: The template content. You use this element when you want to pass the template syntax directly in the request rather than link to an existing template. It can be a JObject or well-formed JSON string. Use either the templateLink property or the template property, but not both.
    - `[(Any) <Object>]`: This indicates any property can be added to this object.
  - `[TemplateLinkContentVersion <String>]`: If included, must match the ContentVersion in the template.
  - `[TemplateLinkId <String>]`: The resource id of a Template Spec. Use either the id or uri property, but not both.
  - `[TemplateLinkQueryString <String>]`: The query string (for example, a SAS token) to be used with the templateLink URI.
  - `[TemplateLinkRelativePath <String>]`: The relativePath property can be used to deploy a linked template at a location relative to the parent. If the parent template was linked with a TemplateSpec, this will reference an artifact in the TemplateSpec.  If the parent was linked with a URI, the child deployment will be a combination of the parent and relativePath URIs
  - `[TemplateLinkUri <String>]`: The URI of the template to deploy. Use either the uri or id property, but not both.
  - `[SystemDataCreatedAt <DateTime?>]`: The timestamp of resource creation (UTC).
  - `[SystemDataCreatedBy <String>]`: The identity that created the resource.
  - `[SystemDataCreatedByType <CreatedByType?>]`: The type of identity that created the resource.
  - `[SystemDataLastModifiedAt <DateTime?>]`: The timestamp of resource last modification (UTC)
  - `[SystemDataLastModifiedBy <String>]`: The identity that last modified the resource.
  - `[SystemDataLastModifiedByType <CreatedByType?>]`: The type of identity that last modified the resource.

## RELATED LINKS

