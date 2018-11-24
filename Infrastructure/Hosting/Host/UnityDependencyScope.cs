// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityDependencyScope.cs">
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

    using global::Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The unity dependency scope.
    /// </summary>
    public class UnityDependencyScope : IDependencyScope, IDisposable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyScope"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UnityDependencyScope(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the container.
        /// </summary>
        protected IUnityContainer Container { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Container.Dispose();
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
                return this.Container.Resolve(serviceType);
            }

            try
            {
                var resolve = this.Container.Resolve(serviceType);
                return resolve;
            }
            catch (ResolutionFailedException ex)
            {
                ILogger logger = this.Container.Resolve<ILogger>();

                logger.Error(
                    () =>
                    $"Unable to resolve {serviceType.FullName} at GetService from container ContainerHash#{this.Container.GetHashCode()}, {ex.Message}, {ex.StackTrace}");

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
        public IEnumerable<object> GetServices(Type serviceType)
        {
            //return this.Container.ResolveAll(serviceType, new ResolverOverride[0]);
            try
            {
                return this.Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        #endregion
    }
}