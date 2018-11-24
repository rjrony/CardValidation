// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extension.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     Extension class
    /// </summary>
    public static class Extension
    {
        /*
        /// <summary>
        /// Warms up database asynchronously.
        ///     It gets the first dbset out of the given context and executes a query against it.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the context.
        /// </typeparam>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The executed Task.
        /// </returns>
        public static async Task WarmUpDbAsync<TContext>(this TContext context) where TContext : ContextBase
        {
            await Task.Run(
                () =>
                    {
                        var foundType = GetFirstFoundEntityType(context);
                        context.Set(foundType).Take(1).ToListAsync();
                    });
        }
        */

        /// <summary>
        /// The get first found entity type.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TContext">
        /// the db context
        /// </typeparam>
        /// <returns>
        /// The <see cref="Type"/>type of the first entity type.
        /// </returns>
        public static Type GetFirstFoundEntityType<TContext>(TContext context)
        {
            //where TContext : ContextBase
            var firstSet = (from p in context.GetType().GetProperties()
                            where
                                p.PropertyType.IsGenericType
                                && (p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                                    || p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>))
                            select p.GetValue(context, null)).FirstOrDefault();

            if (firstSet != null)
            {
                var type = firstSet.GetType().GenericTypeArguments.FirstOrDefault();

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// The get localized value.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="T">
        /// entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetLocalizedValue<T>(this T entity, CultureInfo culture, string property) where T : class
        {
            var language = culture.TwoLetterISOLanguageName.ToCharArray();
            language[0] = char.ToUpper(language[0]);
            var value = typeof(T).GetProperty(property + "_" + new string(language)).GetValue(entity);

            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the context.
        /// </typeparam>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="databaseInitializer">
        /// The database initializer.
        /// </param>
        /// <param name="forceReCreateDatabase">
        /// if set to <c>true</c> [force re create database].
        /// </param>
        /// <param name="setInitializerOnly">
        /// if set to <c>true</c> [set initilizer only].
        /// </param>
        /// <returns>
        /// The used DbInitializer.
        /// </returns>
        public static IDatabaseInitializer<TContext> InitializeDb<TContext>(
            this TContext context,
            IDatabaseInitializer<TContext> databaseInitializer = null,
            bool forceReCreateDatabase = false,
            bool setInitializerOnly = false) where TContext : ContextBase
        {
            if (databaseInitializer == null)
            {
                databaseInitializer = TryGetInitializer(context);
                if (databaseInitializer == null)
                {
                    return null;
                }
            }

            // Set Db Initializer
            Database.SetInitializer(databaseInitializer);

            if (!setInitializerOnly)
            {
                context.Database.Initialize(forceReCreateDatabase);
            }

            return databaseInitializer;
        }

        /// <summary>
        /// Sets the specified context.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the context.
        /// </typeparam>
        /// <typeparam name="TEntity">
        /// The type of the entity.
        /// </typeparam>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="noTracking">
        /// if set to <c>true</c> [no tracking].
        /// </param>
        /// <returns>
        /// The DbSet.
        /// </returns>
        public static DbQuery<TEntity> Set<TContext, TEntity>(this TContext context, bool noTracking) where TEntity : class
            where TContext : ContextBase
        {
            if (noTracking)
            {
                return context.Set<TEntity>().AsNoTracking();
            }

            return context.Set<TEntity>();
        }

        /// <summary>
        /// Tries the cast initializer.
        /// </summary>
        /// <typeparam name="TContext">
        /// The type of the context.
        /// </typeparam>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="initializer">
        /// The initializer.
        /// </param>
        /// <returns>
        /// The concrete DatabaseInitializer.
        /// </returns>
        public static IDatabaseInitializer<TContext> TryCastInitializer<TContext>(this TContext context, object initializer)
            where TContext : ContextBase
        {
            if (initializer == null)
            {
                return null;
            }

            var cast = initializer as IDatabaseInitializer<TContext>;

            if (cast == null)
            {
                throw new ArgumentException("DbInitializer must be of type '{0}'".FormatWith(typeof(IDatabaseInitializer<TContext>)));
            }

            return cast;
        }

        private static IDatabaseInitializer<TContext> TryGetInitializer<TContext>(TContext context) where TContext : ContextBase
        {
            var dbInitializerType =
                CurrentDomainTypes.GetChilrenClassesFromBinDirectory(typeof(DbInitializerBase<,>), true)
                    .FirstOrDefault(t => t.BaseType != null && t.BaseType.GenericTypeArguments.Contains(context.GetType()));
            IDatabaseInitializer<TContext> dbInitializerInstance = null;

            if (dbInitializerType != null)
            {
                dbInitializerInstance = context.TryCastInitializer(Activator.CreateInstance(dbInitializerType));
            }

            return dbInitializerInstance;
        }
    }
}