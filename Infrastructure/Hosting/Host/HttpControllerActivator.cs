// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpControllerActivator.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using global::Infrastructure.Logging.Contracts;

    /// <summary>
    ///     The http controller activator.
    /// </summary>
    public class HttpControllerActivator : IHttpControllerActivator
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpControllerActivator"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public HttpControllerActivator(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="controllerDescriptor">
        /// The controller descriptor.
        /// </param>
        /// <param name="controllerType">
        /// The controller type.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpController"/>.
        /// </returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            this.logger.Debug(() => $"Create controller {controllerType.FullName}");
            var type = request.GetDependencyScope().GetService(controllerType) as IHttpController;
            IHttpController httpController = (IHttpController)type;
            this.logger.Debug(() => $"Controller created {controllerType.FullName}");
            return httpController;
        }
    }
}