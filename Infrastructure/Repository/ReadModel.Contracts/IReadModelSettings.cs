// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadModelSettings.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel.Contracts
{
    using System.ComponentModel;

    /// <summary>
    ///     ReadModel Settings contract.
    /// </summary>
    public interface IReadModelSettings
    {
        /// <summary>
        ///     Gets or sets the catalog suffix.
        /// </summary>
        /// <value>
        ///     The catalog prefix used usally for different environments.
        /// </value>
        string DbCatalogPrefix { get; set; }

        /// <summary>
        ///     Gets or sets the db base catalog name.which is used for catalog name transformations
        /// </summary>
        string DbContextNameReplacement { get; set; }

        /// <summary>
        ///     Gets or sets the name of the data source.
        /// </summary>
        /// <value>
        ///     The name of the data source.
        /// </value>
        string DbDataSourceName { get; set; }

        /// <summary>
        ///     Gets or sets the database initializer.
        /// </summary>
        /// <value>
        ///     The database initializer.
        /// </value>
        object DbInitializer { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [integrated security].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [integrated security]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(true)]
        bool DbIntegratedSecurity { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [database multiple active result sets].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [database multiple active result sets]; otherwise, <c>false</c>.
        /// </value>
        bool DbMultipleActiveResultSets { get; set; }

        /// <summary>
        ///     Gets or sets the database password.
        /// </summary>
        /// <value>
        ///     The database password.
        /// </value>
        string DbPassword { get; set; }

        /// <summary>
        ///     Gets or sets the name of the provider.
        /// </summary>
        /// <value>
        ///     The name of the provider.
        /// </value>
        string DbProviderName { get; set; }

        /// <summary>
        ///     Gets or sets the id of the user.
        /// </summary>
        /// <value>
        ///     The id of the user.
        /// </value>
        string DbUserId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [do not initialize database].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [do not initialize database]; otherwise, <c>false</c>.
        /// </value>
        bool DoNotInitializeDatabase { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [enable SQL logging].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable SQL logging]; otherwise, <c>false</c>.
        /// </value>
        bool EnableSqlLogging { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the database initializer should force recreating the database.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [force re create database]; otherwise, <c>false</c>.
        /// </value>
        bool ForceReCreateDatabase { get; set; }
    }
}