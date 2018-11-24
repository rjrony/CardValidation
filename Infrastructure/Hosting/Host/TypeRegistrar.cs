// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegistrar.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using global::Infrastructure.Interception.Contract;

    using Infrastructure.Host.Contracts;
    using Infrastructure.SessionManagement;
    using Infrastructure.SessionManagement.Contracts;

    /// <summary>
    ///     The unity of work unity registrar.
    /// </summary>
    public class TypeRegistrar : ITypeRegistrar
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        /// The type registrar service.
        /// </param>
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            typeRegistrarService.RegisterTypeUnityOfWork<IRequestInfo, RequestInfo>();
            typeRegistrarService.RegisterTypeUnityOfWork<ITransactionScopeProvider, DefaultAsyncTransactionScope>();
        }
    }
}