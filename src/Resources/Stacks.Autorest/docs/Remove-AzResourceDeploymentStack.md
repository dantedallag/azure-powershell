---
external help file:
Module Name: Az.Resources
online version: https://learn.microsoft.com/powershell/module/az.resources/remove-azresourcedeploymentstack
schema: 2.0.0
---

# Remove-AzResourceDeploymentStack

## SYNOPSIS
Deletes a Deployment Stack by name.
When operation completes, status code 200 returned without content.

## SYNTAX

### Delete1 (Default)
```
Remove-AzResourceDeploymentStack -Name <String> [-SubscriptionId <String>]
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### Delete
```
Remove-AzResourceDeploymentStack -Name <String> -ResourceGroupName <String> [-SubscriptionId <String>]
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### Delete2
```
Remove-AzResourceDeploymentStack -ManagementGroupId <String> -Name <String>
 [-UnmanageActionManagementGroup <UnmanageActionManagementGroupMode>]
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### DeleteViaIdentity
```
Remove-AzResourceDeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### DeleteViaIdentity1
```
Remove-AzResourceDeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

### DeleteViaIdentity2
```
Remove-AzResourceDeploymentStack -InputObject <IDeploymentStacksIdentity>
 [-UnmanageActionManagementGroup <UnmanageActionManagementGroupMode>]
 [-UnmanageActionResource <UnmanageActionResourceMode>]
 [-UnmanageActionResourceGroup <UnmanageActionResourceGroupMode>] [-DefaultProfile <PSObject>] [-AsJob]
 [-NoWait] [-PassThru] [-Confirm] [-WhatIf] [<CommonParameters>]
```

## DESCRIPTION
Deletes a Deployment Stack by name.
When operation completes, status code 200 returned without content.

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

### -DefaultProfile
The DefaultProfile parameter is not functional.
Use the SubscriptionId parameter when available if executing the cmdlet against a different subscription.

```yaml
Type: System.Management.Automation.PSObject
Parameter Sets: (All)
Aliases: AzureRMContext, AzureCredential

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
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.IDeploymentStacksIdentity
Parameter Sets: DeleteViaIdentity, DeleteViaIdentity1, DeleteViaIdentity2
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
Parameter Sets: Delete2
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the deployment stack.

```yaml
Type: System.String
Parameter Sets: Delete, Delete1, Delete2
Aliases: DeploymentStackName

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

### -PassThru
Returns true when the command succeeds

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

### -ResourceGroupName
The name of the resource group.
The name is case insensitive.

```yaml
Type: System.String
Parameter Sets: Delete
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
Parameter Sets: Delete, Delete1
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
Accept pipeline input: False
Accept wildcard characters: False
```

### -UnmanageActionManagementGroup
Flag to indicate delete rather than detach for the management groups.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Support.UnmanageActionManagementGroupMode
Parameter Sets: Delete2, DeleteViaIdentity2
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UnmanageActionResource
Flag to indicate delete rather than detach for the resources.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Support.UnmanageActionResourceMode
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UnmanageActionResourceGroup
Flag to indicate delete rather than detach for the resource groups.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Support.UnmanageActionResourceGroupMode
Parameter Sets: (All)
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

### Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.IDeploymentStacksIdentity

## OUTPUTS

### System.Boolean

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


`INPUTOBJECT <IDeploymentStacksIdentity>`: Identity Parameter
  - `[DeploymentStackName <String>]`: Name of the deployment stack.
  - `[Id <String>]`: Resource identity path
  - `[ManagementGroupId <String>]`: Management Group.
  - `[ResourceGroupName <String>]`: The name of the resource group. The name is case insensitive.
  - `[SubscriptionId <String>]`: The ID of the target subscription.

## RELATED LINKS

