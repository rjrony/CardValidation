// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHostUnityContainer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The HostUnityContainer interface.
    /// </summary>
    public interface IHostUnityContainer : IUnityContainer
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get root container.
        /// </summary>
        /// <returns>
        ///     The <see cref="IUnityContainer" />.
        /// </returns>
        IUnityContainer GetRootContainer();

        #endregion
    }
}