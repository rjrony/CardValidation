// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadModelSettings.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel.Contracts
{
    using System.ComponentModel;

    /// <summary>
    ///     ReadModel settings.
    /// </summary>
    public class ReadModelSettings : IReadModelSettings
    {
        /// <summary>
        ///     Gets or sets the database initializer.
        /// </summary>
        /// <value>
        ///     The database initializer.
        /// </value>
        [DefaultValue(null)]
        public object DbInitializer { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [do not initialize database].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [do not initialize database]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool DoNotInitializeDatabase { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [enable SQL logging].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable SQL logging]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool EnableSqlLogging { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the database initializer should force recreating the database.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [force re create database]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool ForceReCreateDatabase { get; set; }

        #region Connection Settings

        /// <summary>
        ///     Gets or sets the name of the data source.
        /// </summary>
        /// <value>
        ///     The name of the data source.
        /// </value>
        [DefaultValue("(local)")]
        public string DbDataSourceName { get; set; }

        /// <summary>
        ///     Gets or sets the catalog suffix.
        /// </summary>
        /// <value>
        ///     The catalog suffix.
        /// </value>
        public string DbCatalogPrefix { get; set; }

        /// <summary>
        ///     Gets or sets the db base catalog name.which is used for catalog name transformations
        /// </summary>
        public string DbContextNameReplacement { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [integrated security].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [integrated security]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(true)]
        public bool DbIntegratedSecurity { get; set; }

        /// <summary>
        ///     Gets or sets the id of the user.
        /// </summary>
        /// <value>
        ///     The id of the user.
        /// </value>
        public string DbUserId { get; set; }

        /// <summary>
        ///     Gets or sets the database password.
        /// </summary>
        /// <value>
        ///     The database password.
        /// </value>
        public string DbPassword { get; set; }

        /// <summary>
        ///     Gets or sets the name of the provider.
        /// </summary>
        /// <value>
        ///     The name of the provider.
        /// </value>
        [DefaultValue("System.Data.SqlClient")]
        public string DbProviderName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [database multiple active result sets].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [database multiple active result sets]; otherwise, <c>false</c>.
        /// </value>
        public bool DbMultipleActiveResultSets { get; set; }

        #endregion
    }
}