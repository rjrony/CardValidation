// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegistrarPost.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   Unity Registrar
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CardValidation.Repository;
using Infrastructure.Interception.Contract;
using Infrastructure.Repository.Contracts;
using Infrastructure.Testing.Repository;

namespace CardValidation.Api.IntegrationTests
{
    /// <summary>
    ///     Unity Registrar
    /// </summary>
    public class TypeRegistrarPost : ITypeRegistrarPost
    {
        /// <summary>
        ///     The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        ///     The type registrar service.
        /// </param>
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            typeRegistrarService.RegisterTypeSingleton<IDbConnectionFactoryCustom<CardValidationContext>, DbConnectionFactoryTest<CardValidationContext>>();
            typeRegistrarService.RegisterTypeSingleton<IDbConnectionFactoryCustom<CardValidationReadContext>, DbConnectionFactoryTest<CardValidationReadContext>>();
        }
    }
}