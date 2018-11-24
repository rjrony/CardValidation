// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System.Web.Http;

    using global::Infrastructure.Logging.Contracts;

    using Infrastructure.Host.Contracts;
    //using Infrastructure.WebApiSecurity;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The base api controller.
    /// </summary>
    //[AuthorizationDataProcess]
    public class BaseApiController : ApiController
    {
        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        [Dependency]
        public ILogger Logger { get; set; }

        /// <summary>
        ///     The no content.
        /// </summary>
        /// <returns>
        ///     The <see cref="NoContentResult" />.
        /// </returns>
        protected NoContentResult NoContent()
        {
            return new NoContentResult(this);
        }
    }
}