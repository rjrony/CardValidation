// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HostUnityContainer.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using System;
    using System.Collections.Generic;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     Basic Unity Container.
    /// </summary>
    public class HostUnityContainer : IHostUnityContainer
    {
        private static ILogger logger;

        protected readonly UnityContainer RootContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HostUnityContainer" /> class.
        /// </summary>
        public HostUnityContainer()
        {
            this.RootContainer = new UnityContainer();
            var dependencyResolver = new DependencyResolver(this.RootContainer);
            this.RootContainer.RegisterInstance<IDependencyResolver>(dependencyResolver, new HierarchicalLifetimeManager());
            this.RootContainer.RegisterInstance(dependencyResolver, new HierarchicalLifetimeManager());
            //this.RootContainer.RegisterInstance<IHostUnityContainer>(this, new ContainerControlledLifetimeManager());//ToDo comment out due to circular dependancy,when this.RootContainer dispose, it tryes to dispose HostUnityContainer, but this.RootContainer is property of HostUnityContainer
        }

        /// <summary>
        ///     Gets the parent of this container.
        /// </summary>
        /// <value>
        ///     The parent container, or null if this container doesn't have one.
        /// </value>
        public IUnityContainer Parent
        {
            get
            {
                return this.RootContainer.Parent;
            }
        }

        /// <summary>
        ///     Gets a sequence of <see cref="T:Microsoft.Practices.Unity.ContainerRegistration" /> that describe the current state
        ///     of the container.
        /// </summary>
        public IEnumerable<ContainerRegistration> Registrations
        {
            get
            {
                return this.RootContainer.Registrations;
            }
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>
        ///     The logger.
        /// </value>
        protected ILogger Logger
        {
            get
            {
                if (logger == null || logger is NullLogger)
                {
                    logger = this.RootContainer.ResolveSafe<ILogger>(new NullLogger());
                }

                return logger ?? (logger = new NullLogger());
            }
        }

        /// <summary>
        /// Add an extension object to the container.
        /// </summary>
        /// <param name="extension">
        /// <see cref="T:Microsoft.Practices.Unity.UnityContainerExtension"/> to add.
        /// </param>
        /// <returns>
        /// The <see cref="T:Microsoft.Practices.Unity.UnityContainer"/> object that this method was called on (this in C#, Me
        ///     in Visual Basic).
        /// </returns>
        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            return this.RootContainer.AddExtension(extension);
        }

        /// <summary>
        /// Run an existing object through the container and perform injection on it.
        /// </summary>
        /// <param name="t">
        /// <see cref="T:System.Type"/> of object to perform injection on.
        /// </param>
        /// <param name="existing">
        /// Instance to build up.
        /// </param>
        /// <param name="name">
        /// name to use when looking up the typemappings and other configurations.
        /// </param>
        /// <param name="resolverOverrides">
        /// Any overrides for the resolve calls.
        /// </param>
        /// <returns>
        /// The resulting object. By default, this will be <paramref name="existing"/>, but
        ///     container extensions may add things like automatic proxy creation which would
        ///     cause this to return a different object (but still type compatible with <paramref name="t"/>).
        /// </returns>
        /// <remarks>
        /// This method is useful when you don't control the construction of an
        ///     instance (ASP.NET pages or objects created via XAML, for instance)
        ///     but you still want properties and other injection performed.
        /// </remarks>
        public object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides)
        {
            return this.GetContainerForResolve().BuildUp(t, existing, name, resolverOverrides);
        }

        /// <summary>
        /// Resolve access to a configuration interface exposed by an extension.
        /// </summary>
        /// <param name="configurationInterface">
        /// <see cref="T:System.Type"/> of configuration interface required.
        /// </param>
        /// <returns>
        /// The requested extension's configuration interface, or null if not found.
        /// </returns>
        /// <remarks>
        /// Extensions can expose configuration interfaces as well as adding
        ///     strategies and policies to the container. This method walks the list of
        ///     added extensions and returns the first one that implements the requested type.
        /// </remarks>
        public object Configure(Type configurationInterface)
        {
            return this.RootContainer.Configure(configurationInterface);
        }

        /// <summary>
        ///     Create a child container.
        /// </summary>
        /// <returns>
        ///     The new child container.
        /// </returns>
        /// <remarks>
        ///     A child container shares the parent's configuration, but can be configured with different
        ///     settings or lifetime.
        /// </remarks>
        public IUnityContainer CreateChildContainer()
        {
            return this.AddChildContainerContract(this.RootContainer.CreateChildContainer());
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.RootContainer.Dispose();
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.RootContainer.Equals(obj);
        }

        /// <summary>
        ///     The get hash code.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.RootContainer.GetHashCode();
        }

        /// <summary>
        ///     Gets the root container.
        /// </summary>
        /// <returns>The root container.</returns>
        public IUnityContainer GetRootContainer()
        {
            return this.RootContainer;
        }

        /// <summary>
        /// Register an instance with the container.
        /// </summary>
        /// <param name="t">
        /// Type of instance to register (may be an implemented interface instead of the full type).
        /// </param>
        /// <param name="name">
        /// Name for registration.
        /// </param>
        /// <param name="instance">
        /// Object to returned.
        /// </param>
        /// <param name="lifetime">
        /// <see cref="T:Microsoft.Practices.Unity.LifetimeManager"/> object that controls how this
        ///     instance will be managed by the container.
        /// </param>
        /// <returns>
        /// The <see cref="T:Microsoft.Practices.Unity.UnityContainer"/> object that this method was called on (this in C#, Me
        ///     in Visual Basic).
        /// </returns>
        /// <remarks>
        /// Instance registration is much like setting a type as a singleton, except that instead
        ///     of the container creating the instance the first time it is requested, the user
        ///     creates the instance ahead of type and adds that instance to the container.
        /// </remarks>
        public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            return this.RootContainer.RegisterInstance(t, name, instance, lifetime);
        }

        /// <summary>
        /// Register a type mapping with the container, where the created instances will use
        ///     the given <see cref="T:Microsoft.Practices.Unity.LifetimeManager"/>.
        /// </summary>
        /// <param name="from">
        /// <see cref="T:System.Type"/> that will be requested.
        /// </param>
        /// <param name="to">
        /// <see cref="T:System.Type"/> that will actually be returned.
        /// </param>
        /// <param name="name">
        /// Name to use for registration, null if a default registration.
        /// </param>
        /// <param name="lifetimeManager">
        /// The <see cref="T:Microsoft.Practices.Unity.LifetimeManager"/> that controls the lifetime
        ///     of the returned instance.
        /// </param>
        /// <param name="injectionMembers">
        /// Injection configuration objects.
        /// </param>
        /// <returns>
        /// The <see cref="T:Microsoft.Practices.Unity.UnityContainer"/> object that this method was called on (this in C#, Me
        ///     in Visual Basic).
        /// </returns>
        public IUnityContainer RegisterType(
            Type @from,
            Type to,
            string name,
            LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)
        {
            return this.RootContainer.RegisterType(from, to, name, lifetimeManager, injectionMembers);
        }

        /// <summary>
        ///     Remove all installed extensions from this container.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Microsoft.Practices.Unity.UnityContainer" /> object that this method was called on (this in C#, Me
        ///     in Visual Basic).
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         This method removes all extensions from the container, including the default ones
        ///         that implement the out-of-the-box behavior. After this method, if you want to use
        ///         the container again you will need to either readd the default extensions or replace
        ///         them with your own.
        ///     </para>
        ///     <para>
        ///         The registered instances and singletons that have already been set up in this container
        ///         do not get removed.
        ///     </para>
        /// </remarks>
        public IUnityContainer RemoveAllExtensions()
        {
            return this.RootContainer.RemoveAllExtensions();
        }

        /// <summary>
        /// Resolve an instance of the requested type with the given name from the container.
        /// </summary>
        /// <param name="t">
        /// <see cref="T:System.Type"/> of object to get from the container.
        /// </param>
        /// <param name="name">
        /// Name of the object to retrieve.
        /// </param>
        /// <param name="resolverOverrides">
        /// Any overrides for the resolve call.
        /// </param>
        /// <returns>
        /// The retrieved object.
        /// </returns>
        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
            var containerForResolve = this.GetContainerForResolve();
            return containerForResolve.Resolve(t, name, resolverOverrides);

            //try
            //{
            //    var containerForResolve = this.GetContainerForResolve();
            //    return containerForResolve.Resolve(t, name, resolverOverrides);
            //}
            //catch (ResolutionFailedException exception)
            //{
            //    this.Logger.Warning(
            //        () =>
            //        string.Format(
            //            "Resolve failed of type {0} Exception: {1} StackTrace: {2}",
            //            t.FullName,
            //            exception.Message,
            //            exception.StackTrace));
            //    return null;
            //}
        }

        /// <summary>
        /// Return instances of all registered types requested.
        /// </summary>
        /// <param name="t">
        /// The type requested.
        /// </param>
        /// <param name="resolverOverrides">
        /// Any overrides for the resolve calls.
        /// </param>
        /// <returns>
        /// Set of objects of type <paramref name="t"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is useful if you've registered multiple types with the same
        ///         <see cref="T:System.Type"/> but different names.
        ///     </para>
        /// <para>
        /// Be aware that this method does NOT return an instance for the default (unnamed) registration.
        ///     </para>
        /// </remarks>
        public IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides)
        {
            return this.GetContainerForResolve().ResolveAll(t, resolverOverrides);
        }

        /// <summary>
        /// Run an existing object through the container, and clean it up.
        /// </summary>
        /// <param name="o">
        /// The object to tear down.
        /// </param>
        public void Teardown(object o)
        {
            this.RootContainer.Teardown(o);
        }

        /// <summary>
        /// Adds the child container contract.
        /// </summary>
        /// <param name="childUnityContainer">
        /// The unity container.
        /// </param>
        /// <returns>
        /// The child container.
        /// </returns>
        protected IUnityContainer AddChildContainerContract(IUnityContainer childUnityContainer)
        {
            var helper = new ChildContainer(childUnityContainer);
            childUnityContainer.RegisterInstance<IChildContainer>(helper, new HierarchicalLifetimeManager());
            childUnityContainer.RegisterInstance<IDependencyResolver>(new DependencyResolver(childUnityContainer));
            return childUnityContainer;
        }

        /// <summary>
        ///     The get container for resolve.
        /// </summary>
        /// <returns>
        ///     The <see cref="IUnityContainer" />.
        /// </returns>
        protected virtual IUnityContainer GetContainerForResolve()
        {
            return this.RootContainer;
        }
    }
}