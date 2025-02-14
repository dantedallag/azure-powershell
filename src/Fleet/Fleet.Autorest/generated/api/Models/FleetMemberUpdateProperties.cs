// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.Fleet.Models
{
    using static Microsoft.Azure.PowerShell.Cmdlets.Fleet.Runtime.Extensions;

    /// <summary>The updatable properties of the FleetMember.</summary>
    public partial class FleetMemberUpdateProperties :
        Microsoft.Azure.PowerShell.Cmdlets.Fleet.Models.IFleetMemberUpdateProperties,
        Microsoft.Azure.PowerShell.Cmdlets.Fleet.Models.IFleetMemberUpdatePropertiesInternal
    {

        /// <summary>Backing field for <see cref="Group" /> property.</summary>
        private string _group;

        /// <summary>The group this member belongs to for multi-cluster update management.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Fleet.Origin(Microsoft.Azure.PowerShell.Cmdlets.Fleet.PropertyOrigin.Owned)]
        public string Group { get => this._group; set => this._group = value; }

        /// <summary>Creates an new <see cref="FleetMemberUpdateProperties" /> instance.</summary>
        public FleetMemberUpdateProperties()
        {

        }
    }
    /// The updatable properties of the FleetMember.
    public partial interface IFleetMemberUpdateProperties :
        Microsoft.Azure.PowerShell.Cmdlets.Fleet.Runtime.IJsonSerializable
    {
        /// <summary>The group this member belongs to for multi-cluster update management.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Fleet.Runtime.Info(
        Required = false,
        ReadOnly = false,
        Read = true,
        Create = true,
        Update = true,
        Description = @"The group this member belongs to for multi-cluster update management.",
        SerializedName = @"group",
        PossibleTypes = new [] { typeof(string) })]
        string Group { get; set; }

    }
    /// The updatable properties of the FleetMember.
    internal partial interface IFleetMemberUpdatePropertiesInternal

    {
        /// <summary>The group this member belongs to for multi-cluster update management.</summary>
        string Group { get; set; }

    }
}