// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeRegistrarService.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    /// <summary>
    ///     The DependencyRegistrar interface.
    /// </summary>
    public interface ITypeRegistrarService
    {
        /// <summary>
        ///     The register type.
        /// </summary>
        /// <typeparam name="TFrom">
        ///     interface
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     implementation
        /// </typeparam>
        void RegisterType<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        ///     The register type singleton.
        /// </summary>
        /// <typeparam name="TFrom">
        ///     interface
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     implementation
        /// </typeparam>
        void RegisterTypeSingleton<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        ///     The register type unity of work.
        /// </summary>
        /// <typeparam name="TFrom">
        ///     interface
        /// </typeparam>
        /// <typeparam name="TTo">
        ///     implementation
        /// </typeparam>
        void RegisterTypeUnityOfWork<TFrom, TTo>() where TTo : TFrom;
    }
}