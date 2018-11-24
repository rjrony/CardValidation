// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOptimizedRepository.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using Infrastructure.ReadModel.Contracts;
    using Infrastructure.Repository;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    /// The read repository.
    /// </summary>
    /// <typeparam name="TContext">
    /// dbcontext
    /// </typeparam>
    public class ReadOptimizedRepository<TContext> : RepositoryBase<TContext>, IReadOptimizedRepository<TContext>
        where TContext : ContextBase, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOptimizedRepository{TContext}"/> class.
        /// </summary>
        /// <param name="dbContextProvider">
        /// The database context provider.
        /// </param>
        public ReadOptimizedRepository(IDbContextFactory<TContext> dbContextProvider)
            : base(dbContextProvider, true)
        {
            this.ContextInternal.Configuration.AutoDetectChangesEnabled = false;
            this.ContextInternal.Configuration.ProxyCreationEnabled = false;
            this.ContextInternal.Configuration.LazyLoadingEnabled = false;
        }
    }
}