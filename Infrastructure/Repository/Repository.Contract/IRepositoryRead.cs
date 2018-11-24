// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryRead.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    //using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The RepositoryRead interface.
    /// </summary>
    public interface IRepositoryRead : IDisposable
    {
        /// <summary>
        ///     Gets or sets the current culture provider.
        /// </summary>
        //ILanguageSupport LanguageSupport { get; set; }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The searched entity.
        /// </returns>
        IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="select">
        /// The select para.
        /// </param>
        /// <typeparam name="TEntity">
        /// actual table/entity
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// type selected column
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        ///     list return
        /// </returns>
        IQueryable<TReturn> Get<TEntity, TReturn>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TReturn>> @select)
            where TEntity : class;

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The entity.</returns>
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The searched entity.
        /// </returns>
        TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class;

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// The searched entity.
        /// </returns>
        Task<TEntity> GetByIdAsync<TEntity>(params object[] keyValues) where TEntity : class;

        /// <summary>
        /// Searches the full text.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <param name="keywords">
        /// The keywords.
        /// </param>
        /// <returns>
        /// Returned
        /// </returns>
        IQueryable<TEntity> SearchFullText<TEntity>(string keywords) where TEntity : class;
    }
}