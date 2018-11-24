// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryConfiguration.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   Defines the RepositoryConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System.Configuration;
    using System.Data.Entity;

    /// <summary>
    /// The repository configuration.
    /// </summary>
    public class RepositoryConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryConfiguration"/> class.
        /// </summary>
        public RepositoryConfiguration()
        {
            var isSqlLogDisabled = false;
            var isSqlLogEnabledConfig = ConfigurationManager.AppSettings["isSqlLogDisabled"];
            if (!string.IsNullOrWhiteSpace(isSqlLogEnabledConfig))
            {
                bool.TryParse(isSqlLogEnabledConfig, out isSqlLogDisabled);
            }

            this.SetDatabaseLogFormatter(
                (context, writeAction) => new CustomDbFormatter(context, writeAction, isSqlLogDisabled));
        }
    }
}
