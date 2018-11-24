// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityRegistrar.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   Unity Registrar
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception.Contract;
using Infrastructure.ReadModel;
using Infrastructure.ReadModel.Conventions;
using Infrastructure.Repository;
using Infrastructure.Repository.Contracts;

namespace CardValidation.Repository
{
    /// <summary>
    ///     Unity Registrar
    /// </summary>
    public class TypeRegistrar : ITypeRegistrar
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="typeRegistrarService">
        /// The type registrar service.
        /// </param>
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            /*
             if (!typeof(IApplyCatalogNameConvention).IsAssignableFrom(catalogNameConventionType))
            {
                throw new Exception(
                    "'{0}' must implement interface '{1}'".FormatWith(
                        catalogNameConventionType.FullName, 
                        typeof(IApplyCatalogNameConvention).FullName));
            }
             */
            typeRegistrarService.RegisterTypeUnityOfWork<IApplyCatalogNameConvention, ClassicModelNameConvention>();

            typeRegistrarService.RegisterTypeUnityOfWork<IConnectionStringFactory<CardValidationContext>, ConnectionStringFactory<CardValidationContext>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbConnectionFactoryCustom<CardValidationContext>, DbConnectionFactory<CardValidationContext>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbContextFactory<CardValidationContext>, ReadModelContextFactory<CardValidationContext>>();

            typeRegistrarService.RegisterTypeUnityOfWork<IConnectionStringFactory<CardValidationReadContext>, ConnectionStringFactory<CardValidationReadContext>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbConnectionFactoryCustom<CardValidationReadContext>, DbConnectionFactory<CardValidationReadContext>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbContextFactory<CardValidationReadContext>, ReadModelContextFactory<CardValidationReadContext>>();

          
            typeRegistrarService.RegisterType<ITestInfo, TestInfo>();

            typeRegistrarService.RegisterTypeUnityOfWork<IRepositoryReadCardValidation, RepositoryReadCardValidation>();
            typeRegistrarService.RegisterTypeUnityOfWork<IRepositoryCardValidation, RepositoryCardValidation>();
        }

        #endregion
    }
}