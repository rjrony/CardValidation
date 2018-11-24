// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbConnectionFactoryTest.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Testing.Repository
{
    using System.Data.Common;

    using Effort;

    using Infrastructure.Repository.Contracts;

    /// <summary>
    ///     The db connection factory test.
    /// </summary>
    public class DbConnectionFactoryTest<T> : IDbConnectionFactoryCustom<T>
    {
        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        public DbConnection Connection { get; set; }

        /// <summary>
        ///     The create connection.
        /// </summary>
        /// <returns>
        ///     The <see cref="DbConnection" />.
        /// </returns>
        public DbConnection CreateConnection()
        {
            if (this.Connection == null)
            {
                this.Connection = DbConnectionFactory.CreateTransient();
            }

            return this.Connection;
        }
    }
}