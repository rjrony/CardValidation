// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConnectionStringFactory.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    using System.Configuration;

    /// <summary>
    /// The ConnectionStringFactory interface.
    /// </summary>
    /// <typeparam name="TContext">
    /// Db Context
    /// </typeparam>
    public interface IConnectionStringFactory<TContext>
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection String Name.
        /// </param>
        /// <returns>
        /// The db context.
        /// </returns>
        ConnectionStringSettings Create(string connectionStringName);

        //ConnectionStringSettings Create(string settingsPrefix = null);
    }
}