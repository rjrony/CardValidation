// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="TContext">
    /// context of the database
    /// </typeparam>
    public interface IRepository<out TContext> : IRepository
        where TContext : class, new()
    {
    }

    /// <summary>
    ///     The Repository interface.
    /// </summary>
    public interface IRepository : IRepositoryRead
    {
        /// <summary>
        /// The bulk insert.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        Task BulkInsertAsync<TEntity>(List<TEntity> list) where TEntity : class;

        void BulkInsert<TEntity>(List<TEntity> list) where TEntity : class;

        /// <summary>
        ///     The create named locker.
        /// </summary>
        /// <returns>
        ///     The <see cref="INamedLocker" />.
        /// </returns>
        INamedLocker CreateNamedLocker();

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <typeparam name="TEntity">
        /// The entity this method is applied to
        /// </typeparam>
        void Delete<TEntity>(params object[] keyValues) where TEntity : class;

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entityToDelete">
        /// The entity to delete.
        /// </param>
        /// <typeparam name="TEntity">
        /// The entity this method is applied to
        /// </typeparam>
        void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="TEntity">
        /// test
        /// </typeparam>
        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

        /// <summary>
        /// The execute sql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        void ExecuteSql(string sql, params object[] parameters);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="TEntity">
        /// The entity this method is applied to
        /// </typeparam>
        void Insert<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// The insert list.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <typeparam name="TEntity">
        /// en
        /// </typeparam>
        void InsertList<TEntity>(List<TEntity> list) where TEntity : class;

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="TEntity">
        /// the entity being saved
        /// </typeparam>
        void Save<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     The save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        ///     The save changes.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entityToUpdate">
        /// The entity to update.
        /// </param>
        /// <typeparam name="TEntity">
        /// the entity being updated
        /// </typeparam>
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
    }
}