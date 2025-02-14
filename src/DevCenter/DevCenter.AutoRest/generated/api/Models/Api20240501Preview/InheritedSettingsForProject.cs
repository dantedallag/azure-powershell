// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview
{
    using static Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Runtime.Extensions;

    /// <summary>Applicable inherited settings for a project.</summary>
    public partial class InheritedSettingsForProject :
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IInheritedSettingsForProject,
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IInheritedSettingsForProjectInternal
    {

        /// <summary>Internal Acessors for NetworkSetting</summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettings Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IInheritedSettingsForProjectInternal.NetworkSetting { get => (this._networkSetting = this._networkSetting ?? new Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.ProjectNetworkSettings()); set { {_networkSetting = value;} } }

        /// <summary>Internal Acessors for NetworkSettingMicrosoftHostedNetworkEnableStatus</summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.MicrosoftHostedNetworkEnableStatus? Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IInheritedSettingsForProjectInternal.NetworkSettingMicrosoftHostedNetworkEnableStatus { get => ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettingsInternal)NetworkSetting).MicrosoftHostedNetworkEnableStatus; set => ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettingsInternal)NetworkSetting).MicrosoftHostedNetworkEnableStatus = value; }

        /// <summary>Internal Acessors for ProjectCatalogSetting</summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettings Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IInheritedSettingsForProjectInternal.ProjectCatalogSetting { get => (this._projectCatalogSetting = this._projectCatalogSetting ?? new Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.DevCenterProjectCatalogSettings()); set { {_projectCatalogSetting = value;} } }

        /// <summary>Backing field for <see cref="NetworkSetting" /> property.</summary>
        private Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettings _networkSetting;

        /// <summary>Network settings that will be enforced on this project.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Origin(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.PropertyOrigin.Owned)]
        internal Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettings NetworkSetting { get => (this._networkSetting = this._networkSetting ?? new Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.ProjectNetworkSettings()); }

        /// <summary>
        /// Indicates whether pools in this Dev Center can use Microsoft Hosted Networks. Defaults to Enabled if not set.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Origin(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.PropertyOrigin.Inlined)]
        public Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.MicrosoftHostedNetworkEnableStatus? NetworkSettingMicrosoftHostedNetworkEnableStatus { get => ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettingsInternal)NetworkSetting).MicrosoftHostedNetworkEnableStatus; }

        /// <summary>Backing field for <see cref="ProjectCatalogSetting" /> property.</summary>
        private Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettings _projectCatalogSetting;

        /// <summary>Dev Center settings to be used when associating a project with a catalog.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Origin(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.PropertyOrigin.Owned)]
        internal Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettings ProjectCatalogSetting { get => (this._projectCatalogSetting = this._projectCatalogSetting ?? new Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.DevCenterProjectCatalogSettings()); }

        /// <summary>
        /// Whether project catalogs associated with projects in this dev center can be configured to sync catalog items.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Origin(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.PropertyOrigin.Inlined)]
        public Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.CatalogItemSyncEnableStatus? ProjectCatalogSettingCatalogItemSyncEnableStatus { get => ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettingsInternal)ProjectCatalogSetting).CatalogItemSyncEnableStatus; set => ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettingsInternal)ProjectCatalogSetting).CatalogItemSyncEnableStatus = value ?? ((Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.CatalogItemSyncEnableStatus)""); }

        /// <summary>Creates an new <see cref="InheritedSettingsForProject" /> instance.</summary>
        public InheritedSettingsForProject()
        {

        }
    }
    /// Applicable inherited settings for a project.
    public partial interface IInheritedSettingsForProject :
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Runtime.IJsonSerializable
    {
        /// <summary>
        /// Indicates whether pools in this Dev Center can use Microsoft Hosted Networks. Defaults to Enabled if not set.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"Indicates whether pools in this Dev Center can use Microsoft Hosted Networks. Defaults to Enabled if not set.",
        SerializedName = @"microsoftHostedNetworkEnableStatus",
        PossibleTypes = new [] { typeof(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.MicrosoftHostedNetworkEnableStatus) })]
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.MicrosoftHostedNetworkEnableStatus? NetworkSettingMicrosoftHostedNetworkEnableStatus { get;  }
        /// <summary>
        /// Whether project catalogs associated with projects in this dev center can be configured to sync catalog items.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Runtime.Info(
        Required = false,
        ReadOnly = false,
        Description = @"Whether project catalogs associated with projects in this dev center can be configured to sync catalog items.",
        SerializedName = @"catalogItemSyncEnableStatus",
        PossibleTypes = new [] { typeof(Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.CatalogItemSyncEnableStatus) })]
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.CatalogItemSyncEnableStatus? ProjectCatalogSettingCatalogItemSyncEnableStatus { get; set; }

    }
    /// Applicable inherited settings for a project.
    internal partial interface IInheritedSettingsForProjectInternal

    {
        /// <summary>Network settings that will be enforced on this project.</summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IProjectNetworkSettings NetworkSetting { get; set; }
        /// <summary>
        /// Indicates whether pools in this Dev Center can use Microsoft Hosted Networks. Defaults to Enabled if not set.
        /// </summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.MicrosoftHostedNetworkEnableStatus? NetworkSettingMicrosoftHostedNetworkEnableStatus { get; set; }
        /// <summary>Dev Center settings to be used when associating a project with a catalog.</summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Models.Api20240501Preview.IDevCenterProjectCatalogSettings ProjectCatalogSetting { get; set; }
        /// <summary>
        /// Whether project catalogs associated with projects in this dev center can be configured to sync catalog items.
        /// </summary>
        Microsoft.Azure.PowerShell.Cmdlets.DevCenter.Support.CatalogItemSyncEnableStatus? ProjectCatalogSettingCatalogItemSyncEnableStatus { get; set; }

    }
}