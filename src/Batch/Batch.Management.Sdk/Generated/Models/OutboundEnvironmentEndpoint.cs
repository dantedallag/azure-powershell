// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.Batch.Models
{
    using System.Linq;

    /// <summary>
    /// A collection of related endpoints from the same service for which the Batch
    /// service requires outbound access.
    /// </summary>
    public partial class OutboundEnvironmentEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the OutboundEnvironmentEndpoint class.
        /// </summary>
        public OutboundEnvironmentEndpoint()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the OutboundEnvironmentEndpoint class.
        /// </summary>

        /// <param name="category">The type of service that the Batch service connects to.
        /// </param>

        /// <param name="endpoints">The endpoints for this service to which the Batch service makes outbound
        /// calls.
        /// </param>
        public OutboundEnvironmentEndpoint(string category = default(string), System.Collections.Generic.IList<EndpointDependency> endpoints = default(System.Collections.Generic.IList<EndpointDependency>))

        {
            this.Category = category;
            this.Endpoints = endpoints;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets the type of service that the Batch service connects to.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "category")]
        public string Category {get; private set; }

        /// <summary>
        /// Gets the endpoints for this service to which the Batch service makes
        /// outbound calls.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "endpoints")]
        public System.Collections.Generic.IList<EndpointDependency> Endpoints {get; private set; }
    }
}