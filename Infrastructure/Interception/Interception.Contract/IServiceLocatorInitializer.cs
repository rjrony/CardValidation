// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceLocatorInitializer.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    /// <summary>
    ///     The ServiceLocatorInitializer interface.
    /// </summary>
    public interface IServiceLocatorInitializer
    {
        #region Public Methods and Operators

        /// <summary>
        /// The initializ.
        /// </summary>
        /// <param name="hostUnityContainer">
        /// The host unity container.
        /// </param>
        void Initialize(IHostUnityContainer hostUnityContainer);

        #endregion
    }
}