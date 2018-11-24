// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionStringFactory.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using System.Configuration;

    using Infrastructure.Logging.Contracts;
    using Infrastructure.ReadModel.Contracts;
    using Infrastructure.ReadModel.Conventions;
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// The connection string factory.
    /// </summary>
    /// <typeparam name="TContext">
    /// Db Context
    /// </typeparam>
    public class ConnectionStringFactory<TContext> : IConnectionStringFactory<TContext>
        where TContext : ContextBase, new()
    {
        #region Fields

        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringFactory{TContext}"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ConnectionStringFactory(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The <see cref="ConnectionStringSettings"/>.
        /// </returns>
        public ConnectionStringSettings Create(string connectionStringName)
        {
            this.ReadModelSettings = new ReadModelSettings();

            var connectionString = this.ReadModelSettings.BuildConnectionString(connectionStringName);

            this.logger.Info(() => $"Connection string created.");

            return connectionString;
        }

        #region Public Properties

        /// <summary>
        ///     Gets or sets the catalog name convention.
        /// </summary>
        /// <value>
        ///     The catalog name convention.
        /// </value>
        [Dependency]
        public IApplyCatalogNameConvention CatalogNameConvention { get; set; }

        /// <summary>
        ///     Gets the read model settings.
        /// </summary>
        /// <value>
        ///     The read model settings.
        /// </value>
        public IReadModelSettings ReadModelSettings { get; private set; }

        #endregion
    }
}