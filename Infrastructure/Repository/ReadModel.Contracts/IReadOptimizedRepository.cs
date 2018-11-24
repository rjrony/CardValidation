// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadOptimizedRepository.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel.Contracts
{
    using Infrastructure.Repository.Contracts;

    /// <summary>
    /// The ReadOptimizedRepository interface.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context.
    /// </typeparam>
    public interface IReadOptimizedRepository<out TContext> : IRepositoryRead
        where TContext : class, new()
    {
    }
}