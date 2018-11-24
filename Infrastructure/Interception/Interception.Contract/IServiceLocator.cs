// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceLocator.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The ServiceLocator interface.
    /// </summary>
    public interface IServiceLocator
    {
        #region Public Properties

        /// <summary>
        ///     Gets the current.
        /// </summary>
        IUnityContainer Current { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get root container.
        /// </summary>
        /// <returns>
        ///     The <see cref="Microsoft.Practices.Unity.IUnityContainer" />.
        /// </returns>
        IUnityContainer GetRootContainer();

        /// <summary>
        ///     The get unity of work container.
        /// </summary>
        /// <returns>
        ///     The <see cref="Microsoft.Practices.Unity.IUnityContainer" />.
        /// </returns>
        IUnityContainer GetUnityOfWorkContainer();

        /// <summary>
        ///     CleanUp unity of work container.
        /// </summary>
        /// <returns>
        ///     The <see cref="Microsoft.Practices.Unity.IUnityContainer" />.
        /// </returns>
        void CleanUpUnityOfWorkContainer();

        #endregion
    }
}