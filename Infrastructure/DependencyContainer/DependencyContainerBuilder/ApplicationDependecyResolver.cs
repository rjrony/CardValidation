// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDependecyResolver.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.DependencyContainerBuilder
{
    using Infrastructure.DependencyContainerBuilder.Contract;
    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;

    // using Owin;

    /// <summary>
    ///     The locator unity dependecy resolver custom scope.
    /// </summary>
    public class ApplicationDependecyResolver : ScopedApplicationDependecyResolver, IApplicationDependecyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDependecyResolver"/> class.
        /// </summary>
        public ApplicationDependecyResolver(IUnityContainer container) : base(container)
        {

        }

        public static ApplicationDependecyResolver PopulateContainer()
        {
            var hostContainer = new HostUnityContainer();
            ((IServiceLocatorInitializer)ServiceLocator.Instance).Initialize(hostContainer);
            var containerBuilder = new DependencyContainerBuilder(hostContainer);
            var container = containerBuilder.BuildUnityContainer();
            var applicationDependecyResolver = new ApplicationDependecyResolver(container);
            container.RegisterInstance<IApplicationDependecyResolver>(applicationDependecyResolver);
            return applicationDependecyResolver;
        }

        /// <summary>
        ///     The begin scope.
        /// </summary>
        /// <returns>
        ///     The <see cref="ScopedApplicationDependecyResolver" />.
        /// </returns>
        public IScopedApplicationDependecyResolver BeginScope()
        {
            var childContainer = this.Container.CreateChildContainer();
          //  ILogger logger = UnityContainerExtensions.Resolve<ILogger>(childContainer);
          //  logger.Debug(() => $"Container initialized at BeginScope ContainerHash#{childContainer.GetHashCode()}");
            ((IUnitOfWorkContainerSetter)ServiceLocator.Instance).SetUnitOfWorkContainer(childContainer);
            return new ScopedApplicationDependecyResolver(childContainer);
        }
    }
}