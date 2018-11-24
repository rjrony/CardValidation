// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegister.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation
{
    using Infrastructure.Interception.Contract;

    using Infrastructure.Validation.Contracts;

    /// <summary>
    ///     The type register.
    /// </summary>
    public class TypeRegister : ITypeRegistrar
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        /// The type registrar service.
        /// </param>
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            typeRegistrarService.RegisterTypeUnityOfWork<IMessageValidator, MessageValidator>();
        }
    }
}