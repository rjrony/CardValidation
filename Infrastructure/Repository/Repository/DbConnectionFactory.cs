// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbConnectionFactory.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System.Data.Common;
    using System.Data.SqlClient;

    using Infrastructure.Repository.Contracts;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// Responsible to instantiate a DbConnection.
    /// </summary>
    /// <typeparam name="TContext">
    /// db context
    /// </typeparam>
    public class DbConnectionFactory<TContext> : IDbConnectionFactoryCustom<TContext>
    {
        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        public DbConnection Connection { get; set; }

        /// <summary>
        ///     Gets or sets the connection string factory.
        /// </summary>
        [Dependency]
        public IConnectionStringFactory<TContext> ConnectionStringFactory { get; set; }

        /// <summary>
        ///     The create connection.
        /// </summary>
        /// <returns>
        ///     The <see cref="DbConnection" />.
        /// </returns>
        public DbConnection CreateConnection()
        {
            var contextType = typeof(TContext);
            var connectionStringSettings = this.ConnectionStringFactory.Create(contextType.Name);
            DbConnection dbConnection = null;
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
                dbConnection = factory.CreateConnection();
                dbConnection.ConnectionString = connectionStringSettings.ConnectionString;
            }
            catch
            {
                dbConnection = (DbConnection)new SqlConnection(connectionStringSettings.ConnectionString);
            }

            return dbConnection;
        }
    }
}