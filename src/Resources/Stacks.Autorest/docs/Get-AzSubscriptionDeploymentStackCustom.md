---
external help file:
Module Name: Az.Resources
online version: https://learn.microsoft.com/powershell/module/az.resources/get-azsubscriptiondeploymentstackcustom
schema: 2.0.0
---

# Get-AzSubscriptionDeploymentStackCustom

## SYNOPSIS
Retrieves a subscription scoped deployment stack

## SYNTAX

### ListDeploymentStacks (Default)
```
Get-AzSubscriptionDeploymentStackCustom [-Confirm] [-WhatIf] [<CommonParameters>]
```

### GetByNameParameterSetname
```
Get-AzSubscriptionDeploymentStackCustom [-Name] <String> [-Confirm] [-WhatIf] [<CommonParameters>]
```

### GetByResourceIdParameterSetName
```
Get-AzSubscriptionDeploymentStackCustom -ResourceId <String> [-Confirm] [-WhatIf] [<CommonParameters>]
```

## DESCRIPTION
Retrieves a subscription scoped deployment stack

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

### -Name
The name of the DeploymentStack to get

```yaml
Type: System.String
Parameter Sets: GetByNameParameterSetname
Aliases: StackName

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ResourceId
ResourceId of the DeploymentStack to get

```yaml
Type: System.String
Parameter Sets: GetByResourceIdParameterSetName
Aliases: Id

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

