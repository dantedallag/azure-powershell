---
external help file:
Module Name: Az.Resources
online version: https://learn.microsoft.com/powershell/module/az.resources/new-azsubscriptiondeploymentstackcustom
schema: 2.0.0
---

# New-AzSubscriptionDeploymentStackCustom

## SYNOPSIS
Creates a new subscription scoped deployment stack

## SYNTAX

### ByTemplateFileWithNoParameters (Default)
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateFile <String> [-DeleteAll] [-DeleteResourceGroups] [-DeleteResources]
 [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateFileWithParameterFile
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateFile <String> -TemplateParameterFile <String> [-DeleteAll] [-DeleteResourceGroups]
 [-DeleteResources] [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateFileWithParameterObject
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateFile <String> -TemplateParameterObject <Hashtable> [-DeleteAll]
 [-DeleteResourceGroups] [-DeleteResources] [-DenySettingsApplyToChildScopes]
 [-DenySettingsExcludedAction <String[]>] [-DenySettingsExcludedPrincipal <String[]>]
 [-DeploymentResourceGroupName <String>] [-Description <String>] [-Force] [-QueryString <String>]
 [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateFileWithParameterUri
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateFile <String> -TemplateParameterUri <String> [-DeleteAll] [-DeleteResourceGroups]
 [-DeleteResources] [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateSpecWithNoParameters
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateSpecId <String> [-DeleteAll] [-DeleteResourceGroups] [-DeleteResources]
 [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateSpecWithParameterFile
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateSpecId <String> [-DeleteAll] [-DeleteResourceGroups] [-DeleteResources]
 [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateSpecWithParameterObject
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateParameterObject <Hashtable> -TemplateSpecId <String> [-DeleteAll]
 [-DeleteResourceGroups] [-DeleteResources] [-DenySettingsApplyToChildScopes]
 [-DenySettingsExcludedAction <String[]>] [-DenySettingsExcludedPrincipal <String[]>]
 [-DeploymentResourceGroupName <String>] [-Description <String>] [-Force] [-QueryString <String>]
 [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateSpecWithParameterUri
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateParameterFile <String> -TemplateParameterUri <String> -TemplateSpecId <String>
 [-DeleteAll] [-DeleteResourceGroups] [-DeleteResources] [-DenySettingsApplyToChildScopes]
 [-DenySettingsExcludedAction <String[]>] [-DenySettingsExcludedPrincipal <String[]>]
 [-DeploymentResourceGroupName <String>] [-Description <String>] [-Force] [-QueryString <String>]
 [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateUriWithNoParameters
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateUri <String> [-DeleteAll] [-DeleteResourceGroups] [-DeleteResources]
 [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateUriWithParameterFile
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateParameterFile <String> -TemplateUri <String> [-DeleteAll] [-DeleteResourceGroups]
 [-DeleteResources] [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateUriWithParameterObject
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateParameterObject <Hashtable> -TemplateUri <String> [-DeleteAll]
 [-DeleteResourceGroups] [-DeleteResources] [-DenySettingsApplyToChildScopes]
 [-DenySettingsExcludedAction <String[]>] [-DenySettingsExcludedPrincipal <String[]>]
 [-DeploymentResourceGroupName <String>] [-Description <String>] [-Force] [-QueryString <String>]
 [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### ByTemplateUriWithParameterUri
```
New-AzSubscriptionDeploymentStackCustom -DenySettingsMode <PSDenySettingsMode> -Location <String>
 -Name <String> -TemplateParameterUri <String> -TemplateUri <String> [-DeleteAll] [-DeleteResourceGroups]
 [-DeleteResources] [-DenySettingsApplyToChildScopes] [-DenySettingsExcludedAction <String[]>]
 [-DenySettingsExcludedPrincipal <String[]>] [-DeploymentResourceGroupName <String>] [-Description <String>]
 [-Force] [-QueryString <String>] [-Tag <Hashtable>] [-Confirm] [-WhatIf] [<CommonParameters>]
```

## DESCRIPTION
Creates a new subscription scoped deployment stack

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

### -DeleteAll
Signal to delete both resources and resource groups after updating stack.

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

### -DeleteResourceGroups
Signal to delete unmanaged stack resource groups after updating stack.

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

### -DeleteResources
Signal to delete unmanaged stack resources after upating stack.

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

### -DenySettingsApplyToChildScopes
Apply to child scopes.

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

### -DenySettingsExcludedAction
List of role-based management operations that are excluded from the denySettings.
Up to 200 actions are permitted.

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingsExcludedPrincipal
List of AAD principal IDs excluded from the lock.
Up to 5 principals are permitted.

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DenySettingsMode
Mode for DenySettings.
Possible values include: "denyDelete", "denyWriteAndDelete", and "none".

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.PSDenySettingsMode
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeploymentResourceGroupName
The ResourceGroup at which the deployment will be created.
If none is specified, it will default to the subscription level scope of the deployment stack.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Description for the stack.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Force
Do not ask for confirmation when overwriting an existing stack.

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

### -Location
The location the resource resides in.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name of the stack.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: StackName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -QueryString
The query string (for example, a SAS token) to be used with the TemplateUri parameter.
Would be used in case of linked templates

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tag
The tags to put on the deployment.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateFile
TemplateFile to be used to create the stack.

```yaml
Type: System.String
Parameter Sets: ByTemplateFileWithNoParameters, ByTemplateFileWithParameterFile, ByTemplateFileWithParameterObject, ByTemplateFileWithParameterUri
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateParameterFile
Parameter file to use for the template.

```yaml
Type: System.String
Parameter Sets: ByTemplateFileWithParameterFile, ByTemplateSpecWithParameterUri, ByTemplateUriWithParameterFile
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateParameterObject
A hash table which represents the parameters.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: ByTemplateFileWithParameterObject, ByTemplateSpecWithParameterObject, ByTemplateUriWithParameterObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateParameterUri
Location of the Parameter file to use for the template.

```yaml
Type: System.String
Parameter Sets: ByTemplateFileWithParameterUri, ByTemplateSpecWithParameterUri, ByTemplateUriWithParameterUri
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateSpecId
ResourceId of the TemplateSpec to be used to create the stack.

```yaml
Type: System.String
Parameter Sets: ByTemplateSpecWithNoParameters, ByTemplateSpecWithParameterFile, ByTemplateSpecWithParameterObject, ByTemplateSpecWithParameterUri
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateUri
Location of the Template to be used to create the stack.

```yaml
Type: System.String
Parameter Sets: ByTemplateUriWithNoParameters, ByTemplateUriWithParameterFile, ByTemplateUriWithParameterObject, ByTemplateUriWithParameterUri
Aliases:

Required: True
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

### System.String

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStack

## NOTES

ALIASES

## RELATED LINKS

