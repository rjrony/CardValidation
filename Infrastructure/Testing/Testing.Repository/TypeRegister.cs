// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegister.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Testing.Repository
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    ///     The unity of work unity registrar.
    /// </summary>
    public class TypeRegister : ITypeRegistrarPost
    {
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        /// The type registrar service.
        /// </param>
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
           // typeRegistrarService.RegisterTypeSingleton<IDbConnectionFactoryCustom<ContextBase>, DbConnectionFactoryTest<ContextBase>>();
            typeRegistrarService.RegisterTypeSingleton<ITestInfo, TestInfo>();
        }
    }
   
}