// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBInitializerBase.cs" company="">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using Infrastructure.Interception.Contract;
using Infrastructure.Logging;
using Infrastructure.Logging.Contracts;
using Infrastructure.Repository.Contracts;
using Microsoft.Practices.Unity;

namespace Infrastructure.Repository
{
    ////using Infrastructure.T4EFMultilanguage;

    /// <summary>
    /// Custom db initializer that drops a database if it already exists and then runs all migrations
    /// </summary>
    /// <typeparam name="TContext">
    /// Db context type
    /// </typeparam>
    /// <typeparam name="TMigrationsConfiguration">
    /// Migration type
    /// </typeparam>
    public abstract class DbInitializerBase<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext>
        where TContext : DbContext, new() where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        private readonly TMigrationsConfiguration config;
        private readonly IDependencyResolver DependencyResolver;
        private readonly IDbInitializerConfig DbInitializerConfig;

        private DbBackup dbBackup;

        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbInitializerBase{TContext,TMigrationsConfiguration}"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        protected DbInitializerBase(IDependencyResolver dependencyResolver)
        {
            this.DependencyResolver = dependencyResolver;
            this.DbInitializerConfig = dependencyResolver.Resolve<IDbInitializerConfig>();
            this.config = new TMigrationsConfiguration
            {
                AutomaticMigrationsEnabled = false,
                AutomaticMigrationDataLossAllowed = false
            };

            if (this.Logger != null)
            {
                this.Logger.Debug(
                    () =>
                        "Init Db initializer '{0}' using configuration '{1}'".FormatWith(this.GetType().ToString(),
                            this.config.GetType().ToString()));
                this.Logger.Debug(
                    () =>
                        "AutomaticMigrations are {0}".FormatWith(this.config.AutomaticMigrationsEnabled
                            ? "enabled"
                            : "disabled"));
            }
        }

        /// <summary>
        ///     Gets or sets the db backup.
        /// </summary>
        public DbBackup DbBackup
        {
            get { return this.dbBackup ?? (this.dbBackup = this.DependencyResolver.GetCurrentDependencyResolver().Resolve<DbBackup>()); }

            set { this.dbBackup = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is db backup needed.
        /// </summary>
        public bool IsDbBackupNeeded { get; set; }

        /// <summary>
        ///     Gets or sets the test info.
        /// </summary>
        //[Dependency]
        //public ITestInfo TestInfo { get; set; }

        private ILogger Logger
        {
            get
            {
                return this.logger ?? (this.logger = this.DependencyResolver.GetCurrentDependencyResolver().Resolve<ILogger>());
            }
        }

        /// <summary>
        /// Initializes database.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public virtual void InitializeDatabase(TContext context)
        {
            try
            {
                List<string> pendingMigrations;
                if (!this.NeedsMigration(context, out pendingMigrations))
                {
                    this.CheckDatabaseSchemaDirty(context);
                    this.Seed(context);
                    return;
                }

                // ok proceed with the migration
                this.CreateOrMigrateDatabase(context, pendingMigrations);
                this.CheckDatabaseSchemaDirty(context);
            }
            catch (Exception ex)
            {
                var dbMigrationException =
                    new DbMigrationException(
                        "Migrating the database '{0}' not successfull".FormatWith(context.Database.Connection.Database),
                        ex);
                this.Logger.Exception(dbMigrationException);
                throw dbMigrationException;
            }
        }

        private void CheckDatabaseSchemaDirty(TContext context)
        {
            if (this.DbInitializerConfig.ExceptionOnMigrationMissmatch && !context.Database.CompatibleWithModel(false))
            {
                var exception = new InvalidOperationException($"The model backing the {context.GetType().Name} context has changed since the database was created. Consider using Code First Migrations to update the database (http://go.microsoft.com/fwlink/?LinkId=238269).");
                this.Logger.Exception(exception);
                throw exception;
            }
        }

        /// <summary>
        /// Commits changes that are defined in a derived sub-class
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public virtual void Seed(TContext context)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Created full text indexes.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected virtual void CreateFullText(TContext context)
        {
            /*
            var fullTextIndexCreator = new FullTextIndexCreator();
            var sqlStatement = fullTextIndexCreator.CreateFullTextIndexSqlStatement(context.GetType(), this.GetFullTextCatalogName());
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sqlStatement);*/
        }

        /// <summary>
        ///     The get full text catalog name.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        protected abstract string GetFullTextCatalogName();

        private static string GetDbProviderName(TContext context)
        {
            if (context.Database.Connection == null)
            {
                return string.Empty;
            }

            var allSupportedProviders = DbProviderFactories.GetFactoryClasses();
            var connection = context.Database.Connection.GetType().FullName;
            var dbProviderName = connection.Replace(".{0}".FormatWith(connection.Split('.').Last()), string.Empty);

            foreach (DataRow row in allSupportedProviders.Rows)
            {
                if (row["InvariantName"].ToString() == dbProviderName)
                {
                    return dbProviderName;
                }
            }

            return string.Empty;
        }

        private void CreateOrMigrateDatabase(TContext context, List<string> pendingMigrations)
        {
            context.Database.Log = s => this.Logger.Info(() => s);
            this.Logger.Warning(() => "We need to migrate the database to the latest version");
            this.Logger.Info(() => "Targetdatabase is {0}".FormatWith(context.Database.Connection.ConnectionString));
            this.Logger.Info(() => "There are '{0}' pending migrations...".FormatWith(pendingMigrations.Count));
            pendingMigrations.ForEach(s => this.Logger.Info(() => "--> Migration '{0}'...".FormatWith(s)));

            if (this.IsDbBackupNeeded)
            {
                // 1. Do a backup before migrating
                if (!this.DbBackup.Backup(context))
                {
                    throw new DbMigrationException("Database Backup not successfull, will abort migration");
                }
            }

            // 2. Migrate database
            var migrator = new DbMigrator(this.config);

            //  pendingMigrations = migrator.GetPendingMigrations().ToList();
            this.Logger.Info(() => "Applying '{0}' migrations...".FormatWith(pendingMigrations.Count()));
            var testInfo = this.DependencyResolver.Resolve<ITestInfo>();
            if (testInfo != null && testInfo.IsTestEnabled)
            {
                context.Database.CreateIfNotExists();
            }
            else
            {
                migrator.Update();
            }

            var appliedMigrations = pendingMigrations;
            if (appliedMigrations.Any())
            {
                this.Logger.Info(
                    () => "'{0} migrations have been successfully applied'".FormatWith(appliedMigrations.Count()));
                appliedMigrations.ForEach(s => this.Logger.Info(() => "--> Migration '{0}' applied".FormatWith(s)));
            }

            // 3. Apply FullTextSearch and execute seed
            this.Logger.Info(() => "Creating full text search...");
            this.CreateFullText(context);
            this.Logger.Info(() => "Executing Seed method...");
            this.Seed(context);

            context.Database.Log = null;
        }

        private bool NeedsMigration(TContext context, out List<string> pendingMigrations)
        {
            var dbProviderName = GetDbProviderName(context);
            this.config.TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString,
                dbProviderName ?? "System.Data.SqlClient");

            var migrator = new DbMigrator(this.config);

            // lets check if there are pending migrations
            pendingMigrations = migrator.GetPendingMigrations().ToList();

            // if we don't have any migrations added by Add-Migration ... then tell the developer
            if (!pendingMigrations.Any() && !context.Database.Exists())
            {
                throw new DbMigrationException(
                    "There are no migrations in this project! Please scaffold migration by executing 'Add-Migration -Verbose Initial' cmdlet in Package Manager Console");
            }

            if (!pendingMigrations.Any())
            {
                this.logger.Debug(() => "No pending migrations, so go out here");
                return false;
            }

            return true;
        }
    }
}