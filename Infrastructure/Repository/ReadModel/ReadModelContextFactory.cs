// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadModelContextFactory.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using System;
    using System.Data.Common;
    using System.Transactions;

    using Infrastructure.Logging.Contracts;
    using Infrastructure.ReadModel.Contracts;
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Creates a DbContext tailored for the ReadModel.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context.
    /// </typeparam>
    public class ReadModelContextFactory<TContext> : global::Infrastructure.Repository.Contracts.IDbContextFactory<TContext>
        where TContext : ContextBase, new()
    {
        #region Fields

        private readonly ILogger logger;

        ////private readonly IReadModelSettingsProvider<TContext> settingsProvider;
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadModelContextFactory{TContext}"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="dbConnectionFactory">
        /// The db Connection Factory.
        /// </param>
        public ReadModelContextFactory(ILogger logger, IDbConnectionFactoryCustom<TContext> dbConnectionFactory)
        {
            this.DbConnectionFactory = dbConnectionFactory;
            this.logger = logger;

            ////this.settingsProvider = settingsProvider;
        }

        #endregion

        /// <summary>
        ///     Gets or sets the db connection factory.
        /// </summary>
        public IDbConnectionFactoryCustom<TContext> DbConnectionFactory { get; set; }

        #region Public Methods and Operators

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>
        ///     An instance of type "TContext"
        /// </returns>
        public TContext Create()
        {
            var contextType = typeof(TContext);

            if (contextType.GetConstructor(new[] { typeof(DbConnection) }) == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Please make sure that your dbcontext has a contructor with Argument '{0}'. Derive your context from '{1}' base class and implement all base constructors to get all allowed constructors.",
                        typeof(DbConnection).FullName,
                        typeof(ContextBase).FullName));
            }

            var context = Activator.CreateInstance(contextType, this.DbConnectionFactory.CreateConnection()) as TContext;
            this.logger.Info(
                () =>
                $"Database context '{context.GetType()}' created. Using {context.Database.Connection.Database}@{context.Database.Connection.DataSource} to connect");

            return this.ApplySettings(context);
        }

        #endregion

        #region Methods

        private TContext ApplySettings(TContext context)
        {
            ////if (this.ReadModelSettings.EnableSqlLogging)
            if (true)
            {
                context.Database.Log = s => this.logger.Debug(() => s);
            }

            // Doing this in non-transactional scope
            using (var t = new TransactionScope(TransactionScopeOption.Suppress))
            {
                // Set correct initializer and initialize it, if no custom initializer is available the respective Entityframework initializer is used
                this.logger.Debug(() => "Looking for a database initializer...");

                /*
                context.InitializeDb(
                    context.TryCastInitializer(this.ReadModelSettings.DbInitializer), 
                    this.ReadModelSettings.ForceReCreateDatabase, 
                    this.ReadModelSettings.DoNotInitializeDatabase);*/
            }

            return context;
        }

        #endregion

        #region Public Properties

        ///// <summary>
        /////     Gets or sets the catalog name convention.
        ///// </summary>
        ///// <value>
        /////     The catalog name convention.
        ///// </value>
        //[Dependency]
        //public IApplyCatalogNameConvention CatalogNameConvention { get; set; }

        ///// <summary>
        ///// Gets or sets the customer aware convention.
        ///// </summary>
        // [Dependency]
        //public ICustomerAwareConvention CustomerAwareConvention { get; set; }

        /// <summary>
        ///     Gets the read model settings.
        /// </summary>
        /// <value>
        ///     The read model settings.
        /// </value>
        public IReadModelSettings ReadModelSettings { get; private set; }

        ///// <summary>
        /////     Gets or sets the tenant aware convention.
        ///// </summary>
        ///// <value>
        /////     The tenant aware convention.
        ///// </value>
        //[Dependency]
        //public ITenantAwareConvention TenantAwareConvention { get; set; }

        /// <summary>
        ///     Gets or sets the connection string factory.
        /// </summary>
        [Dependency]
        public IConnectionStringFactory<TContext> ConnectionStringFactory { get; set; }

        #endregion
    }
}