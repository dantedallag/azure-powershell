// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.Batch.Models
{
    using System.Linq;

    /// <summary>
    /// Properties used to create a user account on a Linux node.
    /// </summary>
    /// <remarks>
    /// Properties used to create a user account on a Linux node.
    /// </remarks>
    public partial class LinuxUserConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the LinuxUserConfiguration class.
        /// </summary>
        public LinuxUserConfiguration()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the LinuxUserConfiguration class.
        /// </summary>

        /// <param name="uid">The uid and gid properties must be specified together or not at all. If not
        /// specified the underlying operating system picks the uid.
        /// </param>

        /// <param name="gid">The uid and gid properties must be specified together or not at all. If not
        /// specified the underlying operating system picks the gid.
        /// </param>

        /// <param name="sshPrivateKey">The private key must not be password protected. The private key is used to
        /// automatically configure asymmetric-key based authentication for SSH between
        /// nodes in a Linux pool when the pool&#39;s enableInterNodeCommunication property
        /// is true (it is ignored if enableInterNodeCommunication is false). It does
        /// this by placing the key pair into the user&#39;s .ssh directory. If not
        /// specified, password-less SSH is not configured between nodes (no
        /// modification of the user&#39;s .ssh directory is done).
        /// </param>
        public LinuxUserConfiguration(int? uid = default(int?), int? gid = default(int?), string sshPrivateKey = default(string))

        {
            this.Uid = uid;
            this.Gid = gid;
            this.SshPrivateKey = sshPrivateKey;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets the uid and gid properties must be specified together or not
        /// at all. If not specified the underlying operating system picks the uid.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "uid")]
        public int? Uid {get; set; }

        /// <summary>
        /// Gets or sets the uid and gid properties must be specified together or not
        /// at all. If not specified the underlying operating system picks the gid.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "gid")]
        public int? Gid {get; set; }

        /// <summary>
        /// Gets or sets the private key must not be password protected. The private
        /// key is used to automatically configure asymmetric-key based authentication
        /// for SSH between nodes in a Linux pool when the pool&#39;s
        /// enableInterNodeCommunication property is true (it is ignored if
        /// enableInterNodeCommunication is false). It does this by placing the key
        /// pair into the user&#39;s .ssh directory. If not specified, password-less SSH is
        /// not configured between nodes (no modification of the user&#39;s .ssh directory
        /// is done).
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "sshPrivateKey")]
        public string SshPrivateKey {get; set; }
    }
}