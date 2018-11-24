// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestInfo.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Infrastructure.SessionManagement.Contracts;

namespace Infrastructure.SessionManagement
{
    /// <summary>
    ///     The request info.
    /// </summary>
    public class RequestInfo : IRequestInfo
    {
        /// <summary>
        ///     Gets or sets the api secret.
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        ///     Gets or sets the authorization parameter.
        /// </summary>
        public string AuthorizationParameter { get; set; }

        /// <summary>
        ///     Gets or sets the authorization scheme.
        /// </summary>
        public string AuthorizationScheme { get; set; }

        /// <summary>
        ///     Gets or sets the Client Type.
        /// </summary>
        public ClientType ClientType { get; set; }

        /// <summary>
        ///     Gets or sets the device id.
        /// </summary>
        public string DeviceClientId { get; set; }

        /// <summary>
        ///     Gets or sets the headers.
        /// </summary>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }

        /// <summary>
        ///     Gets or sets the host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        ///     Gets or sets the tenant id.
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the request Ip address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the correlation id.
        /// </summary>
        public string CorrelationId { get; set; }
    }
}