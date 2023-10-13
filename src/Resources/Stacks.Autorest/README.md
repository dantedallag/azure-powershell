<!-- region Generated -->
# Az.DeploymentStacks
This directory contains the PowerShell module for the DeploymentStacks service.

---
## Status
[![Az.DeploymentStacks](https://img.shields.io/powershellgallery/v/Az.DeploymentStacks.svg?style=flat-square&label=Az.DeploymentStacks "Az.DeploymentStacks")](https://www.powershellgallery.com/packages/Az.DeploymentStacks/)

## Info
- Modifiable: yes
- Generated: all
- Committed: yes
- Packaged: yes

---
## Detail
This module was primarily generated via [AutoRest](https://github.com/Azure/autorest) using the [PowerShell](https://github.com/Azure/autorest.powershell) extension.

## Module Requirements
- [Az.Accounts module](https://www.powershellgallery.com/packages/Az.Accounts/), version 2.2.3 or greater

## Authentication
AutoRest does not generate authentication code for the module. Authentication is handled via Az.Accounts by altering the HTTP payload before it is sent.

## Development
For information on how to develop for `Az.DeploymentStacks`, see [how-to.md](how-to.md).
<!-- endregion -->

### AutoRest Configuration
> see https://aka.ms/autorest

``` yaml
require:
  - $(this-folder)/../../readme.azure.noprofile.md
input-file:
  - https://github.com/Azure/azure-rest-api-specs/blob/c3a00cf753f01728e49bdb232054b0964075ec45/specification/resources/resource-manager/Microsoft.Resources/preview/2022-08-01-preview/deploymentStacks.json

root-module-name: $(prefix).Resources
title: DeploymentStacks
namespace: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks
identity-correction-for-post: true
resourcegroup-append: true
default-exclude-tableview-properties: false

directive:
  # Hide default generated cmdlets:
  - where:
      verb: New
      subject: DeploymentStack
    hide: true
  - where:
      verb: Get
      subject: DeploymentStack
    hide: true
  - where:
      verb: Export
      subject: DeploymentStackTemplate
    hide: true
  - where:
      verb: Remove
      subject: DeploymentStack
    hide: true
  - where:
      verb: Set
      subject: DeploymentStack
    hide: true

  # Fix Deny Setting parameter names:
  - where:
      parameter-name: DenySettingMode
    set:
      parameter-name: DenySettingsMode
  - where:
      parameter-name: DenySettingExcludedPrincipal
    set:
      parameter-name: DenySettingsExcludedPrincipal
  - where:
      parameter-name: DenySettingExcludedAction
    set:
      parameter-name: DenySettingsExcludedAction
  - where:
      parameter-name: DenySettingApplyToChildScope
    set:
      parameter-name: DenySettingsApplyToChildScopes

  # Changes for models to generate hashtables:
  - from: swagger-document
    where: $.definitions.DeploymentStackProperties.properties.template
    transform: $['additionalProperties'] = true;
  - from: swagger-document
    where: $.definitions.DeploymentStackProperties.properties.parameters
    transform: $['additionalProperties'] = true;
  - from: swagger-document
    where: $.definitions.DeploymentStackTemplateDefinition.properties.template
    transform: $['additionalProperties'] = true;

  # Deployment Stack model property renames:

  # Top level Error renames:
  - where:
      model-name: DeploymentStack
      property-name: Code
    set:
      property-name: ErrorCode
  - where:
      model-name: DeploymentStack
      property-name: Message
    set:
      property-name: ErrorMessage
  - where:
      model-name: DeploymentStack
      property-name: Target
    set:
      property-name: ErrorTarget

  # ActionOnUnmange renames:
  - where:
      model-name: DeploymentStack
      property-name: ActionOnUnmanageResourceGroup
    set:
      property-name: ResourceGroupsCleanupAction
  - where:
      model-name: DeploymentStack
      property-name: ActionOnUnmanageResource
    set:
      property-name: ResourcesCleaunupAction
  - where:
      model-name: DeploymentStack
      property-name: ActionOnUnmanageManagementGroup
    set:
      property-name: ManagementGroupCleanupAction

  # List renames to plural:
  - where:
      model-name: DeploymentStack
      property-name: Resource
    set:
      property-name: Resources
  - where:
      model-name: DeploymentStack
      property-name: FailedResource
    set:
      property-name: FailedResources
  - where:
      model-name: DeploymentStack
      property-name: DetachedResource
    set:
      property-name: DetachedResources
  - where:
      model-name: DeploymentStack
      property-name: DeletedResource
    set:
      property-name: DeletedResources
  - where:
      model-name: DeploymentStack
      property-name: DenySettingMode
    set:
      property-name: DenySettingsMode
  - where:
      model-name: DeploymentStack
      property-name: DenySettingApplyToChildScope
    set:
      property-name: DenySettingsApplyToChildScopes
  - where:
      model-name: DeploymentStack
      property-name: DenySettingExcludedAction
    set:
      property-name: DenySettingsExcludedActions
  - where:
      model-name: DeploymentStack
      property-name: DenySettingExcludedPrincipal
    set:
      property-name: DenySettingsExcludedPrincipals  
  - where:
      model-name: DeploymentStack
      property-name: Parameter
    set:
      property-name: Parameters
  - where:
      model-name: DeploymentStack
      property-name: ParameterLinkUri
    set:
      property-name: ParametersLinkUri
  - where:
      model-name: DeploymentStack
      property-name: ParameterLinkContentVersion
    set:
      property-name: ParametersLinkContentVersion
  - where:
      model-name: DeploymentStack
      property-name: Tag
    set:
      property-name: Tags
  - where:
      model-name: DeploymentStack
      property-name: Output
    set:
      property-name: Outputs

  - where:
      parameter-name: Tag
    set:
      type: Microsoft.Azure.PowerShell.Cmdlets.Resources.DeploymentStacks.Models.Api20220801Preview.DeploymentStackTag


# TODO: We need to get insight on the best way to adjust the view for the stack model
  # - where:
  #     model-name: DeploymentStack
  #   set:
  #     format-table:
  #       properties:
  #       - Name
  #       - DenySettingsMode
  #       - FailedResources
  #       labels:
  #         Name: Name
  #         DenySettingsMode: DenySettingsMode
```
