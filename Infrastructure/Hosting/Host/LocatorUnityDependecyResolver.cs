// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocatorUnityDependecyResolver.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Web.Http.Dependencies;

    using global::Infrastructure.Interception;
    using  global::Infrastructure.Interception.Contract;
    using global::Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

    /// <summary>
    ///     The locator unity dependency resolver.
    /// </summary>
    public class LocatorUnityDependencyResolver : UnityDependencyScope, IDependencyResolver, IDependencyScope, IDisposable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorUnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public LocatorUnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The begin scope.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDependencyScope" />.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var childContainer = this.Container.CreateChildContainer();
            ILogger logger = childContainer.Resolve<ILogger>();
            logger.Debug(() => $"Container initialized at BeginScope ContainerHash#{childContainer.GetHashCode()}");
            ((IUnitOfWorkContainerSetter)ServiceLocator.Instance).SetUnitOfWorkContainer(childContainer);
            return new UnityDependencyScope(childContainer);
            /*
            var childContainer = this.Container.CreateChildContainer();
            
            return new UnityDependencyScope(childContainer);
            */
        }

        #endregion
    }
}