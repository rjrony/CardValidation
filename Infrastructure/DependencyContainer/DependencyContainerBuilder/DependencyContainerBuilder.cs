namespace Infrastructure.DependencyContainerBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Infrastructure.DependencyContainerBuilder.Contract;
    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging;
    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    public class DependencyContainerBuilder : IDependencyContainerBuilder
    {

        public IHostUnityContainer UnityContainer;

        private ITypeRegistrarService TypeRegistrarService;

        private static volatile bool isServiceLocatorInitialized = false;

        public DependencyContainerBuilder(HostUnityContainer unityContainer)
        {

            //    this.InitializeServiceLocator(unityContainer);
            this.UnityContainer = unityContainer;
            this.TypeRegistrarService = new TypeRegistrarService(this.UnityContainer);
            //  var scopeHostUnityContainer = new HostUnityContainer();

        }
        public IHostUnityContainer BuildUnityContainer()
        {
            this.RegisterLogger(this.UnityContainer);

            var logger = this.UnityContainer.Resolve<ILogger>();

            this.CheckAssemlbyLoad(logger);

            this.LoadCurrentDomainTypes(logger);

            this.RegisterTypesUsingTypeRegistrar();

            this.SubscribeTypeLoad();

            return this.UnityContainer;
        }

        private void RegisterLogger(IUnityContainer container)
        {
            var logger = container.ResolveSafe<ILogger>(new NullLogger());
            if (logger.IsNull())
            {
                IOperationContext operationContext = new OperationContext { CorrelationId = "APPLICATION-START" };
                container.RegisterInstance(operationContext, new HierarchicalLifetimeManager());
                container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            }
        }

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

            logger.Debug(
                () =>
                $"Count of loaded types in actual domain {AppDomain.CurrentDomain.FriendlyName} {loadedTypes.Count}");
        }

        private void RegisterTypesUsingTypeRegistrar()
        {
            var logger = this.UnityContainer.Resolve<ILogger>();
            var registrarInstances = GetRegistrarInstances<ITypeRegistrar>(logger);

            foreach (var registrar in registrarInstances)
            {
                logger.Debug(() => $"Execute Registrar {registrar.GetType().FullName}");
                registrar.Register(this.TypeRegistrarService);
            }

            var registrarInstancesPost = GetRegistrarInstances<ITypeRegistrarPost>(logger);

            foreach (var registrar in registrarInstancesPost)
            {
                logger.Debug(() => $"Execute Registrar Post {registrar.GetType().FullName}");
                registrar.Register(this.TypeRegistrarService);
            }
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


        private void InitializeServiceLocator(IHostUnityContainer hostUnityContainer)
        {
            if (isServiceLocatorInitialized)
            {
                return;
            }

    ((IServiceLocatorInitializer)ServiceLocator.Instance).Initialize(hostUnityContainer);
            isServiceLocatorInitialized = true;
        }


        private void SubscribeTypeLoad()
        {
            AppDomain.CurrentDomain.TypeResolve += this.OnCurrentDomainTypeResolve;
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

    }

    public static class Extention
    {
        public static bool IsNull<T>(this T subject)
        {
            return ReferenceEquals(subject, null);
        }
    }



}
