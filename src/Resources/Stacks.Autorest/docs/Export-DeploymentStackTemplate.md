---
external help file:
Module Name: DeploymentStacks
online version: https://learn.microsoft.com/powershell/module/deploymentstacks/export-deploymentstacktemplate
schema: 2.0.0
---

# Export-DeploymentStackTemplate

## SYNOPSIS
Exports the template used to create the deployment stack.

## SYNTAX

### Export1 (Default)
```
Export-DeploymentStackTemplate -DeploymentStackName <String> -SubscriptionId <String> [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### Export
```
Export-DeploymentStackTemplate -DeploymentStackName <String> -ResourceGroupName <String>
 -SubscriptionId <String> [-Confirm] [-WhatIf] [<CommonParameters>]
```

### Export2
```
Export-DeploymentStackTemplate -DeploymentStackName <String> -ManagementGroupId <String> [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### ExportViaIdentity
```
Export-DeploymentStackTemplate -InputObject <IDeploymentStacksIdentity> [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### ExportViaIdentity1
```
Export-DeploymentStackTemplate -InputObject <IDeploymentStacksIdentity> [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### ExportViaIdentity2
```
Export-DeploymentStackTemplate -InputObject <IDeploymentStacksIdentity> [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

## DESCRIPTION
Exports the template used to create the deployment stack.

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
Name of the deployment stack.

```yaml
Type: System.String
Parameter Sets: Export, Export1, Export2
Aliases:

Required: True
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
Parameter Sets: ExportViaIdentity, ExportViaIdentity1, ExportViaIdentity2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ManagementGroupId
Management Group.

```yaml
Type: System.String
Parameter Sets: Export2
Aliases:

Required: True
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
Parameter Sets: Export
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
Parameter Sets: Export, Export1
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

### Sample.API.Models.IDeploymentStacksIdentity

## OUTPUTS

### Sample.API.Models.IDeploymentStackTemplateDefinition

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


`INPUTOBJECT <IDeploymentStacksIdentity>`: Identity Parameter
  - `[DeploymentStackName <String>]`: Name of the deployment stack.
  - `[ManagementGroupId <String>]`: Management Group.
  - `[ResourceGroupName <String>]`: The name of the resource group. The name is case insensitive.
  - `[SubscriptionId <String>]`: The ID of the target subscription.

## RELATED LINKS

