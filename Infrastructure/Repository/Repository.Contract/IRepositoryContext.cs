// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryContext.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    /// <summary>
    /// The RepositoryContext interface.
    /// </summary>
    /// <typeparam name="TContext">
    /// type of the context
    /// </typeparam>
    public interface IRepositoryContext<out TContext>
        where TContext : class, new()
    {
        /// <summary>
        ///     Gets the context.
        /// </summary>
        TContext Context { get; }
    }
}