// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    /// Read-Write repository.
    /// </summary>
    /// <typeparam name="TContext">
    /// dbcontext
    /// </typeparam>
    public class Repository<TContext> : RepositoryBase<TContext>
        where TContext : ContextBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TContext}"/> class.
        /// </summary>
        /// <param name="dbContextProvider">
        /// The database context provider.
        /// </param>
        public Repository(IDbContextFactory<TContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}