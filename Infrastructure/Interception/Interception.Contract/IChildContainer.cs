// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChildContainer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    using Microsoft.Practices.Unity;

    /// <summary>
    ///     The ChildContainer interface.
    /// </summary>
    public interface IChildContainer
    {
        #region Public Properties

        /// <summary>
        ///     Gets the container.
        /// </summary>
        IUnityContainer Container { get; }

        #endregion
    }
}