// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiDependencyScope.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dependencies;

    using global::Host.Owin;

    using Infrastructure.DependencyContainerBuilder;
    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    internal class WebApiDependencyScope : IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiDependencyScope"/> class.
        /// </summary>
        /// <param name="dependecyResolver">
        /// The dependecy resolver.
        /// </param>
        public WebApiDependencyScope(ScopedApplicationDependecyResolver dependecyResolver)
        {
            this.DependecyResolver = dependecyResolver;
        }

        private ScopedApplicationDependecyResolver DependecyResolver { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.DependecyResolver.Dispose();
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetService(Type serviceType)
        {

            if (serviceType.GetInterfaces().Contains(typeof(IHttpController)))
            {
                return this.DependecyResolver.Resolve(serviceType);
            }

            try
            {
                var resolve = this.DependecyResolver.Resolve(serviceType);
                return resolve;
            }
            catch (ResolutionFailedException ex)
            {
                ILogger logger = this.DependecyResolver.Resolve<ILogger>();

                logger.Warning(
                    () =>
                    $"Unable to resolve {serviceType.FullName} at GetService from container ContainerHash#{this.DependecyResolver.GetHashCode()}, {ex.Message}, {ex.StackTrace}");

                return null;
            }

        }

        /// <summary>
        /// The get services.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.DependecyResolver.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }

        }
    }
}