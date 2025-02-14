// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401
{
    using static Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Extensions;

    /// <summary>Event details for InMageRcmFailback provider.</summary>
    public partial class InMageRcmFailbackEventDetails :
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetails,
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal,
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.IValidates
    {
        /// <summary>
        /// Backing field for Inherited model <see cref= "Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetails"
        /// />
        /// </summary>
        private Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetails __eventProviderSpecificDetails = new Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.EventProviderSpecificDetails();

        /// <summary>Backing field for <see cref="ApplianceName" /> property.</summary>
        private string _applianceName;

        /// <summary>The appliance name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Owned)]
        public string ApplianceName { get => this._applianceName; }

        /// <summary>Backing field for <see cref="ComponentDisplayName" /> property.</summary>
        private string _componentDisplayName;

        /// <summary>The component display name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Owned)]
        public string ComponentDisplayName { get => this._componentDisplayName; }

        /// <summary>Gets the class type. Overridden in derived classes.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Inherited)]
        public string InstanceType { get => ((Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetailsInternal)__eventProviderSpecificDetails).InstanceType; set => ((Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetailsInternal)__eventProviderSpecificDetails).InstanceType = value ; }

        /// <summary>Internal Acessors for ApplianceName</summary>
        string Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal.ApplianceName { get => this._applianceName; set { {_applianceName = value;} } }

        /// <summary>Internal Acessors for ComponentDisplayName</summary>
        string Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal.ComponentDisplayName { get => this._componentDisplayName; set { {_componentDisplayName = value;} } }

        /// <summary>Internal Acessors for ProtectedItemName</summary>
        string Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal.ProtectedItemName { get => this._protectedItemName; set { {_protectedItemName = value;} } }

        /// <summary>Internal Acessors for ServerType</summary>
        string Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal.ServerType { get => this._serverType; set { {_serverType = value;} } }

        /// <summary>Internal Acessors for VMName</summary>
        string Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IInMageRcmFailbackEventDetailsInternal.VMName { get => this._vMName; set { {_vMName = value;} } }

        /// <summary>Backing field for <see cref="ProtectedItemName" /> property.</summary>
        private string _protectedItemName;

        /// <summary>The protected item name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Owned)]
        public string ProtectedItemName { get => this._protectedItemName; }

        /// <summary>Backing field for <see cref="ServerType" /> property.</summary>
        private string _serverType;

        /// <summary>The server type.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Owned)]
        public string ServerType { get => this._serverType; }

        /// <summary>Backing field for <see cref="VMName" /> property.</summary>
        private string _vMName;

        /// <summary>The protected item name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Origin(Microsoft.Azure.PowerShell.Cmdlets.Migrate.PropertyOrigin.Owned)]
        public string VMName { get => this._vMName; }

        /// <summary>Creates an new <see cref="InMageRcmFailbackEventDetails" /> instance.</summary>
        public InMageRcmFailbackEventDetails()
        {

        }

        /// <summary>Validates that this object meets the validation criteria.</summary>
        /// <param name="eventListener">an <see cref="Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.IEventListener" /> instance that will receive validation
        /// events.</param>
        /// <returns>
        /// A <see cref = "global::System.Threading.Tasks.Task" /> that will be complete when validation is completed.
        /// </returns>
        public async global::System.Threading.Tasks.Task Validate(Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.IEventListener eventListener)
        {
            await eventListener.AssertNotNull(nameof(__eventProviderSpecificDetails), __eventProviderSpecificDetails);
            await eventListener.AssertObjectIsValid(nameof(__eventProviderSpecificDetails), __eventProviderSpecificDetails);
        }
    }
    /// Event details for InMageRcmFailback provider.
    public partial interface IInMageRcmFailbackEventDetails :
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.IJsonSerializable,
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetails
    {
        /// <summary>The appliance name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"The appliance name.",
        SerializedName = @"applianceName",
        PossibleTypes = new [] { typeof(string) })]
        string ApplianceName { get;  }
        /// <summary>The component display name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"The component display name.",
        SerializedName = @"componentDisplayName",
        PossibleTypes = new [] { typeof(string) })]
        string ComponentDisplayName { get;  }
        /// <summary>The protected item name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"The protected item name.",
        SerializedName = @"protectedItemName",
        PossibleTypes = new [] { typeof(string) })]
        string ProtectedItemName { get;  }
        /// <summary>The server type.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"The server type.",
        SerializedName = @"serverType",
        PossibleTypes = new [] { typeof(string) })]
        string ServerType { get;  }
        /// <summary>The protected item name.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Migrate.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Description = @"The protected item name.",
        SerializedName = @"vmName",
        PossibleTypes = new [] { typeof(string) })]
        string VMName { get;  }

    }
    /// Event details for InMageRcmFailback provider.
    internal partial interface IInMageRcmFailbackEventDetailsInternal :
        Microsoft.Azure.PowerShell.Cmdlets.Migrate.Models.Api202401.IEventProviderSpecificDetailsInternal
    {
        /// <summary>The appliance name.</summary>
        string ApplianceName { get; set; }
        /// <summary>The component display name.</summary>
        string ComponentDisplayName { get; set; }
        /// <summary>The protected item name.</summary>
        string ProtectedItemName { get; set; }
        /// <summary>The server type.</summary>
        string ServerType { get; set; }
        /// <summary>The protected item name.</summary>
        string VMName { get; set; }

    }
}