// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeRegistrarPost.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interception.Contract
{
    /// <summary>
    ///     The ITypeRegistrarPost interface.
    /// </summary>
    public interface ITypeRegistrarPost
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        /// The type registrar service.
        /// </param>
        void Register(ITypeRegistrarService typeRegistrarService);

        #endregion
    }
}