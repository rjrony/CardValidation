// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestInfo.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The RequestInfo interface.
    /// </summary>
    public interface IRequestInfo : IOperationContext
    {
        /// <summary>
        ///     Gets or sets the api secret.
        /// </summary>
        string ApiSecret { get; set; }

        /// <summary>
        ///     Gets or sets the authorization.
        /// </summary>
        string AuthorizationParameter { get; set; }

        /// <summary>
        ///     Gets or sets the authorization scheme.
        /// </summary>
        string AuthorizationScheme { get; set; }

        /// <summary>
        ///     Gets or sets the Client Type.
        /// </summary>
        ClientType ClientType { get; set; }

        /// <summary>
        ///     Gets or sets the device id.
        /// </summary>
        string DeviceClientId { get; set; }

        /// <summary>
        ///     Gets or sets the headers.
        /// </summary>
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }

        /// <summary>
        ///     Gets or sets the host name.
        /// </summary>
        string HostName { get; set; }

        /// <summary>
        ///     Gets or sets the tenant id.
        /// </summary>
        Guid? TenantId { get; set; }

        /// <summary>
        /// Get or sets the request Ip Address
        /// </summary>
        string IpAddress { get; set; }

    }
}