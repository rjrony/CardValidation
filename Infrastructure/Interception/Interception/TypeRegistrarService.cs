// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegistrarService.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception
{
    using Infrastructure.Interception.Contract;

    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The type registrar.
    /// </summary>
    public class TypeRegistrarService : ITypeRegistrarService
    {
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeRegistrarService"/> class.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        public TypeRegistrarService(IUnityContainer unityContainer)
        {
            this.container = unityContainer;
        }

        /// <summary>
        ///     The register type.
        /// </summary>
        /// <typeparam name="TFrom">
        /// </typeparam>
        /// <typeparam name="TTo">
        /// </typeparam>
        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            this.container.RegisterType(typeof(TFrom), typeof(TTo));
        }

        /// <summary>
        ///     The register type singleton.
        /// </summary>
        /// <typeparam name="TFrom">
        ///     interface
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     implementation
        /// </typeparam>
        public void RegisterTypeSingleton<TFrom, TTo>() where TTo : TFrom
        {
            this.container.RegisterType(typeof(TFrom), typeof(TTo), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        ///     The register type unity of work.
        /// </summary>
        /// <typeparam name="TFrom">
        ///     interface
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     implementation
        /// </typeparam>
        public void RegisterTypeUnityOfWork<TFrom, TTo>() where TTo : TFrom
        {
            this.container.RegisterType(typeof(TFrom), typeof(TTo), new HierarchicalLifetimeManager());
        }
    }
}