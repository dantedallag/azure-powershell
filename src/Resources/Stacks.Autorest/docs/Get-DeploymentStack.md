---
external help file:
Module Name: DeploymentStacks
online version: https://learn.microsoft.com/powershell/module/deploymentstacks/get-deploymentstack
schema: 2.0.0
---

# Get-DeploymentStack

## SYNOPSIS
Gets a Deployment Stack with a given name.

## SYNTAX

### List1 (Default)
```
Get-DeploymentStack -SubscriptionId <String> [<CommonParameters>]
```

### Get
```
Get-DeploymentStack -DeploymentStackName <String> -ResourceGroupName <String> -SubscriptionId <String>
 [<CommonParameters>]
```

### Get1
```
Get-DeploymentStack -DeploymentStackName <String> -SubscriptionId <String> [<CommonParameters>]
```

### Get2
```
Get-DeploymentStack -DeploymentStackName <String> -ManagementGroupId <String> [<CommonParameters>]
```

### GetViaIdentity
```
Get-DeploymentStack -InputObject <IDeploymentStacksIdentity> [<CommonParameters>]
```

### GetViaIdentity1
```
Get-DeploymentStack -InputObject <IDeploymentStacksIdentity> [<CommonParameters>]
```

### GetViaIdentity2
```
Get-DeploymentStack -InputObject <IDeploymentStacksIdentity> [<CommonParameters>]
```

### List
```
Get-DeploymentStack -ResourceGroupName <String> -SubscriptionId <String> [<CommonParameters>]
```

### List2
```
Get-DeploymentStack -ManagementGroupId <String> [<CommonParameters>]
```

## DESCRIPTION
Gets a Deployment Stack with a given name.

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
Parameter Sets: Get, Get1, Get2
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
Parameter Sets: GetViaIdentity, GetViaIdentity1, GetViaIdentity2
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
Parameter Sets: Get2, List2
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
Parameter Sets: Get, List
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
Parameter Sets: Get, Get1, List, List1
Aliases:

Required: True
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

### Sample.API.Models.IDeploymentStack

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

