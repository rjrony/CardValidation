// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbContextFactory.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    /// <summary>
    /// Interface for DbContextFactory implementations.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context.
    /// </typeparam>
    public interface IDbContextFactory<out TContext>
    {
        /// <summary>
        ///     The create.
        /// </summary>
        /// <returns>
        ///     The <see cref="TContext" />.
        /// </returns>
        TContext Create();

        ////TContext Create(string settingsPrefix = null);
    }
}