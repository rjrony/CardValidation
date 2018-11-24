// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbConnectionFactoryCustom.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    using System.Data.Common;

    /// <summary>
    ///     The DbConnectionFactoryCustom interface.
    /// </summary>
    public interface IDbConnectionFactoryCustom<T>
    {
        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        DbConnection Connection { get; set; }

        /// <summary>
        ///     The create connection.
        /// </summary>
        /// <returns>
        ///     The <see cref="DbConnection" />.
        /// </returns>
        DbConnection CreateConnection();
    }
}