// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using System.Configuration;

    using Infrastructure.ReadModel.Contracts;

    /// <summary>
    ///     Extensions for ReadModel.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The build connection string.
        /// </summary>
        /// <param name="modelSettings">
        /// The model settings.
        /// </param>
        /// <param name="connectionStringName">
        /// The connection String Name.
        /// </param>
        /// <returns>
        /// The <see cref="ConnectionStringSettings"/>.
        /// </returns>
        public static ConnectionStringSettings BuildConnectionString(this IReadModelSettings modelSettings, string connectionStringName)
        {
            var settings = ConfigurationManager.ConnectionStrings[connectionStringName];
            return settings;
        }
    }
}