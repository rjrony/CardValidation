// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyContainerBuilder.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DependencyContainerBuilder.Contract
{
    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The DependencyContainerBuilder interface.
    /// </summary>
    public interface IDependencyContainerBuilder
    {
        /// <summary>
        /// The build unity container.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        /// <returns>
        /// The <see cref="IHostUnityContainer"/>.
        /// </returns>
        IHostUnityContainer BuildUnityContainer();
    }
}