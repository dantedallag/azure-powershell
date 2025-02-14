// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview
{
    using static Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Extensions;

    /// <summary>The properties of the dryrun job</summary>
    public partial class DryrunProperties
    {

        /// <summary>
        /// <c>AfterFromJson</c> will be called after the json deserialization has finished, allowing customization of the object
        /// before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>

        partial void AfterFromJson(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject json);

        /// <summary>
        /// <c>AfterToJson</c> will be called after the json serialization has finished, allowing customization of the <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject"
        /// /> before it is returned. Implement this method in a partial class to enable this behavior
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>

        partial void AfterToJson(ref Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject container);

        /// <summary>
        /// <c>BeforeFromJson</c> will be called before the json deserialization has commenced, allowing complete customization of
        /// the object before it is deserialized.
        /// If you wish to disable the default deserialization entirely, return <c>true</c> in the <paramref name= "returnNow" />
        /// output parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="json">The JsonNode that should be deserialized into this object.</param>
        /// <param name="returnNow">Determines if the rest of the deserialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeFromJson(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject json, ref bool returnNow);

        /// <summary>
        /// <c>BeforeToJson</c> will be called before the json serialization has commenced, allowing complete customization of the
        /// object before it is serialized.
        /// If you wish to disable the default serialization entirely, return <c>true</c> in the <paramref name="returnNow" /> output
        /// parameter.
        /// Implement this method in a partial class to enable this behavior.
        /// </summary>
        /// <param name="container">The JSON container that the serialization result will be placed in.</param>
        /// <param name="returnNow">Determines if the rest of the serialization should be processed, or if the method should return
        /// instantly.</param>

        partial void BeforeToJson(ref Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject container, ref bool returnNow);

        /// <summary>
        /// Deserializes a Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject into a new instance of <see cref="DryrunProperties" />.
        /// </summary>
        /// <param name="json">A Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject instance to deserialize from.</param>
        internal DryrunProperties(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject json)
        {
            bool returnNow = false;
            BeforeFromJson(json, ref returnNow);
            if (returnNow)
            {
                return;
            }
            {_parameter = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject>("parameters"), out var __jsonParameters) ? Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.DryrunParameters.FromJson(__jsonParameters) : Parameter;}
            {_prerequisiteResult = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonArray>("prerequisiteResults"), out var __jsonPrerequisiteResults) ? If( __jsonPrerequisiteResults as Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonArray, out var __v) ? new global::System.Func<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunPrerequisiteResult[]>(()=> global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(__v, (__u)=>(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunPrerequisiteResult) (Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.DryrunPrerequisiteResult.FromJson(__u) )) ))() : null : PrerequisiteResult;}
            {_operationPreview = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonArray>("operationPreviews"), out var __jsonOperationPreviews) ? If( __jsonOperationPreviews as Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonArray, out var __q) ? new global::System.Func<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunOperationPreview[]>(()=> global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(__q, (__p)=>(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunOperationPreview) (Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.DryrunOperationPreview.FromJson(__p) )) ))() : null : OperationPreview;}
            {_provisioningState = If( json?.PropertyT<Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonString>("provisioningState"), out var __jsonProvisioningState) ? (string)__jsonProvisioningState : (string)ProvisioningState;}
            AfterFromJson(json);
        }

        /// <summary>
        /// Deserializes a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode"/> into an instance of Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunProperties.
        /// </summary>
        /// <param name="node">a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode" /> to deserialize from.</param>
        /// <returns>
        /// an instance of Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunProperties.
        /// </returns>
        public static Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Models.Api20221101Preview.IDryrunProperties FromJson(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode node)
        {
            return node is Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject json ? new DryrunProperties(json) : null;
        }

        /// <summary>
        /// Serializes this instance of <see cref="DryrunProperties" /> into a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode" />.
        /// </summary>
        /// <param name="container">The <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject"/> container to serialize this object into. If the caller
        /// passes in <c>null</c>, a new instance will be created and returned to the caller.</param>
        /// <param name="serializationMode">Allows the caller to choose the depth of the serialization. See <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.SerializationMode"/>.</param>
        /// <returns>
        /// a serialized instance of <see cref="DryrunProperties" /> as a <see cref="Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode" />.
        /// </returns>
        public Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode ToJson(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject container, Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.SerializationMode serializationMode)
        {
            container = container ?? new Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonObject();

            bool returnNow = false;
            BeforeToJson(ref container, ref returnNow);
            if (returnNow)
            {
                return container;
            }
            AddIf( null != this._parameter ? (Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode) this._parameter.ToJson(null,serializationMode) : null, "parameters" ,container.Add );
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.SerializationMode.IncludeReadOnly))
            {
                if (null != this._prerequisiteResult)
                {
                    var __w = new Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.XNodeArray();
                    foreach( var __x in this._prerequisiteResult )
                    {
                        AddIf(__x?.ToJson(null, serializationMode) ,__w.Add);
                    }
                    container.Add("prerequisiteResults",__w);
                }
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.SerializationMode.IncludeReadOnly))
            {
                if (null != this._operationPreview)
                {
                    var __r = new Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.XNodeArray();
                    foreach( var __s in this._operationPreview )
                    {
                        AddIf(__s?.ToJson(null, serializationMode) ,__r.Add);
                    }
                    container.Add("operationPreviews",__r);
                }
            }
            if (serializationMode.HasFlag(Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.SerializationMode.IncludeReadOnly))
            {
                AddIf( null != (((object)this._provisioningState)?.ToString()) ? (Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonNode) new Microsoft.Azure.PowerShell.Cmdlets.ServiceLinker.Runtime.Json.JsonString(this._provisioningState.ToString()) : null, "provisioningState" ,container.Add );
            }
            AfterToJson(ref container);
            return container;
        }
    }
}