// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeRegistrar.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Repository
{
    using Infrastructure.Interception.Contract;
    using Infrastructure.ReadModel;
    using Infrastructure.ReadModel.Conventions;
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    ///     The type registrar.
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
            typeRegistrarService.RegisterTypeUnityOfWork<IApplyCatalogNameConvention, ClassicModelNameConvention>();
            typeRegistrarService.RegisterTypeUnityOfWork<IConnectionStringFactory<Bb_Monitoring>, ConnectionStringFactory<Bb_Monitoring>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbConnectionFactoryCustom<Bb_Monitoring>, DbConnectionFactory<Bb_Monitoring>>();
            typeRegistrarService.RegisterTypeUnityOfWork<IDbContextFactory<Bb_Monitoring>, ReadModelContextFactory<Bb_Monitoring>>();

            typeRegistrarService.RegisterTypeUnityOfWork<ILoggingRepositoryRead, LoggingRepositoryRead>();
            typeRegistrarService.RegisterTypeUnityOfWork<ILoggingRepository, LoggingRepository>();
        }
    }
}