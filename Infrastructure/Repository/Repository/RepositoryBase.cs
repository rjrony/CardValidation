// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    //using Infrastructure.Localization.Contracts;
    using Infrastructure.Logging.Contracts;
    using Infrastructure.Repository.Contracts;

    /// <summary>
    /// The repository base.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context.
    /// </typeparam>
    public abstract class RepositoryBase<TContext> : IRepository<TContext>, IRepositoryContext<TContext>
        where TContext : ContextBase, new()
    {
        private const string CacheKeySeparator = "_";

        private const string FullTextColumnsContainerName = "FullTextColumnsContainers";

        private readonly bool doNotTrack;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TContext}"/> class.
        /// </summary>
        /// <param name="doNotTrack">
        /// if set to <c>true</c> all queries will be called with .AsNoTracking().
        /// </param>
        protected RepositoryBase(bool doNotTrack = false)
        {
            this.doNotTrack = doNotTrack;
            this.ContextInternal = new TContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TContext}"/> class.
        /// </summary>
        /// <param name="contextProvider">
        /// The context provider.
        /// </param>
        /// <param name="doNotTrack">
        /// The do not track.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// ex happen
        /// </exception>
        protected RepositoryBase(Contracts.IDbContextFactory<TContext> contextProvider,
            bool doNotTrack = false)
        {
            this.doNotTrack = doNotTrack;
            if (contextProvider == null)
            {
                throw new ArgumentNullException("contextProvider");
            }

            this.ContextInternal = contextProvider.Create();
        }

        /*
        /// <summary>
        ///     Gets or sets the cache instance that stores repository related entries: list of multilanguage columns etc.
        /// </summary>
        [Dependency(FullTextColumnsContainerName)]
        public static ICache CacheInstance { get; set; }
        */

        /// <summary>
        ///     Gets or sets the current culture provider.
        /// </summary>
        ////[Dependency]
        //public ILanguageSupport LanguageSupport { get; set; }

        /// <summary>
        ///     Gets the context.
        /// </summary>
        /// <value>
        ///     The context.
        /// </value>
        TContext IRepositoryContext<TContext>.Context
        {
            get
            {
                return this.ContextInternal;
            }
        }

        /// <summary>
        ///     Gets the context internal.
        /// </summary>
        protected TContext ContextInternal { get; private set; }

        

        /// <summary>
        ///     The create locker.
        /// </summary>
        /// <returns>
        ///     The <see cref="INamedLocker" />.
        /// </returns>
        public INamedLocker CreateNamedLocker()
        {
            return new NamedRepositoryLocker<TContext>(this, new NullLogger());
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <typeparam name="TEntity">
        /// Entity use for this method
        /// </typeparam>
        public virtual void Delete<TEntity>(params object[] keyValues) where TEntity : class
        {
            var entityToDelete = this.ContextInternal.Set<TEntity>().Find(keyValues);
            if (entityToDelete != null)
            {
                this.Delete(entityToDelete);
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entityToDelete">
        /// The entity to delete.
        /// </param>
        /// <typeparam name="TEntity">
        /// Entity use for this method
        /// </typeparam>
        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            var dbEntry = this.ContextInternal.Entry(entityToDelete);
            if (dbEntry == null || dbEntry.State == EntityState.Detached)
            {
                this.ContextInternal.Set<TEntity>().Attach(entityToDelete);
            }

            this.ContextInternal.Set<TEntity>().Remove(entityToDelete);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="TEntity">
        /// actual object
        /// </typeparam>
        public void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            foreach (var entityToDelete in this.ContextInternal.Set<TEntity>().Where(filter))
            {
                this.Delete(entityToDelete);
            }
        }

        /// <summary>
        ///     Dispose the context
        /// </summary>
        public void Dispose()
        {
            if (this.ContextInternal != null)
            {
                this.ContextInternal.Dispose();
            }

            this.ContextInternal = null;
        }

        /// <summary>
        /// The execute sql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void ExecuteSql(string sql, params object[] parameters)
        {
            this.ContextInternal.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql, parameters);
        }

        /// <summary>
        /// Get the entities
        /// </summary>
        /// <typeparam name="TEntity">
        /// entity type
        /// </typeparam>
        /// <param name="filter">
        /// where clauses
        /// </param>
        /// <returns>
        /// return querable
        /// </returns>
        public virtual IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return this.ContextInternal.Set<TContext, TEntity>(this.doNotTrack).Where(filter);
        }

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
        public virtual IQueryable<TReturn> Get<TEntity, TReturn>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TReturn>> @select) where TEntity : class
        {
            return this.ContextInternal.Set<TContext, TEntity>(this.doNotTrack).Where(filter).Select(@select);
        }

        /// <summary>
        ///     The create query.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     Entity use for this method
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return this.ContextInternal.Set<TContext, TEntity>(this.doNotTrack);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <typeparam name="TEntity">
        /// The entity this method is applied to
        /// </typeparam>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public TEntity GetById<TEntity>(params object[] keyValues) where TEntity : class
        {
            return this.ContextInternal.Set<TEntity>().Find(keyValues);
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<TEntity> GetByIdAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            return this.ContextInternal.Set<TEntity>().FindAsync(keyValues);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="TEntity">
        /// Entity use for this method
        /// </typeparam>
        public virtual void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            this.ContextInternal.Set<TEntity>().Add(entity);
        }

        public virtual Task BulkInsertAsync<TEntity>(List<TEntity> list) where TEntity : class
        {
            return this.ContextInternal.BulkInsertAsync(list);
        }

        public void BulkInsert<TEntity>(List<TEntity> list) where TEntity : class
        {
            this.ContextInternal.BulkInsert(list);
        }

        /// <summary>
        /// The insert list.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <typeparam name="TEntity">
        /// en
        /// </typeparam>
        public virtual void InsertList<TEntity>(List<TEntity> list) where TEntity : class
        {
            foreach (var entity in list)
            {
                this.ContextInternal.Set<TEntity>().Add(entity);
            }
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="TEntity">
        /// Entity use for this method
        /// </typeparam>
        public virtual void Save<TEntity>(TEntity entity) where TEntity : class
        {
            var objContext = ((IObjectContextAdapter)this.ContextInternal).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            if (exists)
            {
                objContext.Detach(foundEntity);
                this.ContextInternal.Set<TEntity>().Attach(entity);
                this.ContextInternal.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                this.ContextInternal.Set<TEntity>().Add(entity);
            }
        }

        /// <summary>
        ///     The save changes.
        /// </summary>
        public virtual void SaveChanges()
        {
            this.ContextInternal.SaveChanges();
        }

        /// <summary>
        ///     The save changes.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual Task<int> SaveChangesAsync()
        {
            return this.ContextInternal.SaveChangesAsync();
        }

        /// <summary>
        /// Search full text.
        /// </summary>
        /// <param name="keywords">
        /// The keywords.
        /// </param>
        /// <typeparam name="TEntity">
        /// domain entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TEntity> SearchFullText<TEntity>(string keywords) where TEntity : class
        {
            var queryable = this.ContextInternal.Set<TEntity>();
            if (!string.IsNullOrEmpty(keywords))
            {
                var sqlStatement = string.Empty;

                /*
                this.CheckDependencies();
                sqlStatement = this.MakeSqlStatement<TEntity>(keywords);
                */
                if (this.doNotTrack)
                {
                    return queryable.SqlQuery(sqlStatement).AsNoTracking().AsQueryable();
                }

                return queryable.SqlQuery(sqlStatement).AsQueryable();
            }

            return queryable;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entityToUpdate">
        /// The entity to update.
        /// </param>
        /// <typeparam name="TEntity">
        /// Entity use for this method
        /// </typeparam>
        public virtual void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            this.ContextInternal.Set<TEntity>().Attach(entityToUpdate);
            this.ContextInternal.Entry(entityToUpdate).State = EntityState.Modified;
        }

        private static string ReplaceLastOccurrence(string source, string find, string replace)
        {
            var place = source.LastIndexOf(find, StringComparison.Ordinal);

            if (place == -1)
            {
                return string.Empty;
            }

            var result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        /// <summary>
        /// breakdown keywords
        /// </summary>
        /// <param name="keywords">
        /// seach data
        /// </param>
        /// <returns>
        /// return the keywords
        /// </returns>
        private static string SetKeywordsAndWildCard(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return null;
            }

            var words = keywords.Replace("\"", string.Empty).Replace("'", "''").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < words.Length; i++)
            {
                stringBuilder.Append(string.Format("\"{0}\"", words[i]));
                if (i != words.Length - 1)
                {
                    stringBuilder.Append(" AND ");
                }
            }

            return ReplaceLastOccurrence(stringBuilder.ToString(), "\"", "*\"");
        }

        /*
        private void CheckDependencies()
        {
            if (CacheInstance == null)
            {
                CacheInstance = CachingFactory.Instance.CreateCache(FullTextColumnsContainerName);
            }

            if (this.LanguageSupport == null)
            {
                throw new Exception(
                    "languageSupport is null. Please ensure the Repository is resolved using DI Container");
            }
        }
        
        private string MakeSqlStatement<TEntity>(string keyword) where TEntity : class
        {
            string sql = string.Empty;
            Type type = typeof(TEntity);
            string tableName = type.Name;

            var columsList =
                CacheInstance.Get(
                    tableName.ToLower() + CacheKeySeparator
                    + this.LanguageSupport.GetCurrentLanguage().CultureInfo.TwoLetterISOLanguageName.ToLower()) as string;

            if (columsList == null || string.IsNullOrEmpty(columsList))
            {
                columsList = type.GetColumnsList(this.LanguageSupport);
                CacheInstance.Add(
                    tableName.ToLower() + CacheKeySeparator
                    + this.LanguageSupport.GetCurrentLanguage().CultureInfo.TwoLetterISOLanguageName.ToLower(), 
                    columsList);
            }

            keyword = SetKeywordsAndWildCard(keyword);

            if (!string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(columsList))
            {
                sql = string.Format("SELECT * FROM {0} T WHERE CONTAINS(T.*, '{1}')", tableName, keyword);
            }
            else if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(columsList))
            {
                sql = string.Format("SELECT * FROM {0} WHERE CONTAINS(({2}), '{1}')", tableName, keyword, columsList);
            }

            return sql;
        }
        */
    }
}