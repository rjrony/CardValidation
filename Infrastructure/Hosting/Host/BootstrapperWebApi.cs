// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapperWebApi.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.SessionManagement;

namespace Infrastructure.Host
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    //using global::Host.Owin;

    using Infrastructure;
    using Infrastructure.DependencyContainerBuilder;
    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.Host.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The bootstrapper web api.
    /// </summary>
    public class BootstrapperWebApi : IBootstrapper
    {
        #region Static Fields

        private static volatile bool isServiceLocatorInitialized = false;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperWebApi"/> class.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public BootstrapperWebApi(HttpConfiguration config)
        {
            //config = GlobalConfiguration.Configuration; //ToDo this code will be removed, we will totally decoupled from System.Web
            var scopeHostUnityContainer = new ScopeHostUnityContainer();
            this.InitializeServiceLocator(scopeHostUnityContainer);
            /*
            var rootContainer = new UnityContainer();
            this.RegisterLogger(rootContainer);
            ILogger logger= rootContainer.Resolve<ILogger>();
            logger.Debug(()=>$"Container initialized ContainerHash#{rootContainer.GetHashCode()}");
            DependencyResolver dependencyResolver = new DependencyResolver(rootContainer);
            rootContainer.RegisterInstance<IDependencyResolver>(dependencyResolver, new HierarchicalLifetimeManager());
            rootContainer.RegisterInstance(dependencyResolver, new HierarchicalLifetimeManager());*/
            this.Config = new BootstrapperConfig
            {
                Configuration = config,
                Container = ServiceLocator.Instance.Current,
                IsAutoRegisterUnityOfWork = true,
                Bootstrapper = this,
                TypeRegistrarService = new TypeRegistrarService(ServiceLocator.Instance.Current)
            };
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the config.
        /// </summary>
        protected BootstrapperConfig Config { get; private set; }

        public ApplicationDependecyResolver DependencyResolver { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The run.
        /// </summary>
        /// <param name="isLoggingEnabled">
        /// The is logging enabled.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public virtual IBootstrapperConfig Initialize(bool isLoggingEnabled = false)
        {
            this.Config.IsLoggingEnabled = isLoggingEnabled;
            this.SetupContainer();
            this.HandleUnhandledExceptions(this.Config);
            this.HandleApplicationShutdown(this.Config);
            //this.RegisterRequestInfoHandler(this.Config);
            //this.RegisterLoggingHandler(this.Config);
            //this.SetDefaultCachingConfiguration(this.Config);
            return this.Config;
        }

        /// <summary>
        ///     The build unity container.
        /// </summary>
        protected virtual void BuildUnityContainer()
        {
            this.RegisterLogger(this.Config.Container);

            var logger = this.Config.Container.Resolve<ILogger>();

            this.RegisterControllers(this.Config.Container);
            this.CheckAssemlbyLoad(logger);
            //this.RegisterTypes(this.Config.Container, logger);
            this.LoadCurrentDomainTypes(logger);

            this.RegisterTypesUsingTypeRegistrar();

            this.SubscribeTypeLoad();
        }

        /// <summary>
        /// The register controllers.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        protected virtual void RegisterControllers(IUnityContainer container)
        {
            var logger = this.Config.Container.Resolve<ILogger>();
            // Register our http controller activator with NSB
            container.RegisterInstance<IHttpControllerActivator>(
                new HttpControllerActivator(logger),
                new ContainerControlledLifetimeManager());
        }

        /// <summary>
        ///     The setup container.
        /// </summary>
        protected virtual void SetupContainer()
        {
            this.BuildUnityContainer();
            var locatorUnityDependencyResolver = new ApplicationDependecyResolver(this.Config.Container);
            this.DependencyResolver = locatorUnityDependencyResolver;
            //GlobalConfiguration.Configuration.DependencyResolver = new LocatorUnityDependencyResolver(this.Config.Container);
            //GlobalConfiguration.Configuration.DependencyResolver = locatorUnityDependencyResolver;
            //  this.Config.Configuration.DependencyResolver = locatorUnityDependencyResolver;
            //  this.Config.Configuration.DependencyResolver = locatorUnityDependencyResolver;
        }

        private static IEnumerable<T> GetRegistrarInstances<T>(ILogger logger)
        {
            try
            {
                logger.Debug(() => string.Format("Look for {0} in the current domain", typeof(T)));

                var registrarTypes = CurrentDomainTypes.GetTypesDerivingFrom<T>(isIncludingAbstract: false);

                logger.Debug(() => string.Format("Found {0} types count {1}", typeof(T), registrarTypes.Count()));
                return registrarTypes.Select(registrarType => (T)Activator.CreateInstance(registrarType)).ToList();
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                logger.Fatal(reflectionTypeLoadException);
                throw;
            }
        }

        /// <summary>
        /// If the assemblies can't be loaded, this indicated a bad setup of the project
        ///     and must be solved before going on
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        private void CheckAssemlbyLoad(ILogger logger)
        {
            try
            {
                this.LoadCurrentDomainTypes(logger);
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                logger.Fatal(typeLoadException);
                throw;
            }
            catch (Exception exception)
            {
                logger.Fatal(exception);
                throw;
            }
        }

        private void HandleApplicationShutdown(BootstrapperConfig bootstrapperConfig)
        {
            AppDomain.CurrentDomain.DomainUnload += (sender, args) => AppShutDownHandler.ReportShutDownReason(bootstrapperConfig.Logger());
        }

        private void HandleUnhandledExceptions(BootstrapperConfig bootstrapperConfig)
        {
            AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs args)
                {
                    var exception = args.ExceptionObject as Exception;
                    if (exception != null)
                    {
                        bootstrapperConfig.Logger().Fatal(exception);
                    }
                };
        }

        private void InitializeServiceLocator(IHostUnityContainer hostUnityContainer)
        {
            if (isServiceLocatorInitialized)
            {
                return;
            }

            ((IServiceLocatorInitializer)ServiceLocator.Instance).Initialize(hostUnityContainer);
            isServiceLocatorInitialized = true;
        }

        /// <summary>
        /// Loads all types in the current domain and caches them
        /// </summary>
        /// <param name="logger">
        /// Logger
        /// </param>
        private void LoadCurrentDomainTypes(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            var entryAssembly = EnvironmentInfo.GetEntryAssembly();
            if (entryAssembly != null)
            {
                logger.Debug(() => $"Add entry assembly to CurrentDomainTypes {entryAssembly.FullName}");
                var assemblyName = entryAssembly.GetName();
                CurrentDomainTypes.AddAssembly(assemblyName);
            }

            var loadedTypes = CurrentDomainTypes.GetTypes().ToList();

            logger.Debug(() => $"Count of loaded types in actual domain {AppDomain.CurrentDomain.FriendlyName} {loadedTypes.Count}");
        }

        private Assembly OnCurrentDomainTypeResolve(object sender, ResolveEventArgs args)
        {
            var foundType = CurrentDomainTypes.GetTypes().FirstOrDefault(t => t.FullName == args.Name || t.Name == args.Name);
            if (foundType != null)
            {
                return foundType.Assembly;
            }

            return null;
        }

        private void RegisterLogger(IUnityContainer container)
        {
            var logger = container.ResolveSafe<ILogger>(new NullLogger());
            if (logger.IsNull())
            {
                var operationContext = (IOperationContext) new RequestInfo {CorrelationId = "APPLICATION-START"};
                container.RegisterInstance(operationContext, new HierarchicalLifetimeManager());
                container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            }
        }

        //private void RegisterRequestInfoHandler(BootstrapperConfig bootstrapperConfig)
        //{
        //    var handler = bootstrapperConfig.Container.Resolve<RequestHandler>();
        //    //GlobalConfiguration.Configuration.MessageHandlers.Add(handler);
        //    this.Config.Configuration.MessageHandlers.Add(handler);
        //}
        private void RegisterTypesUsingTypeRegistrar()
        {
            var logger = this.Config.Container.Resolve<ILogger>();
            var registrarInstances = GetRegistrarInstances<ITypeRegistrar>(logger);

            foreach (var registrar in registrarInstances)
            {
                logger.Debug(() => $"Execute Registrar {registrar.GetType().FullName}");
                registrar.Register(this.Config.TypeRegistrarService);
            }

            var registrarInstancesPost = GetRegistrarInstances<ITypeRegistrarPost>(logger);

            foreach (var registrar in registrarInstancesPost)
            {
                logger.Debug(() => $"Execute Registrar Post {registrar.GetType().FullName}");
                registrar.Register(this.Config.TypeRegistrarService);
            }
        }

        private void SubscribeTypeLoad()
        {
            AppDomain.CurrentDomain.TypeResolve += this.OnCurrentDomainTypeResolve;
        }





        #endregion
    }
}