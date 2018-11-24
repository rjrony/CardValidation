// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWorkContainerSetter.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The UnitOfWorkContainerSetter interface.
    /// </summary>
    public interface IUnitOfWorkContainerSetter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The set unit of work container.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        void SetUnitOfWorkContainerThreadIndepended(IUnityContainer container);

        /// <summary>
        /// Set The unit of work thread static
        /// </summary>
        /// <param name="container">
        /// The container
        /// </param>
        void SetUnitOfWorkContainer(IUnityContainer container);

        #endregion
    }
}