// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextBase.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository
{
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>
    ///     Base class for all dbcontexts.
    /// </summary>
    [DbConfigurationType(typeof(RepositoryConfiguration))]
    public abstract class ContextBase : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContextBase" /> class.
        /// </summary>
        protected ContextBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextBase"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// Either the database name or a connection string.
        /// </param>
        protected ContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextBase"/> class.
        /// </summary>
        /// <param name="existingConnection">
        /// An existing connection to use for the new context.
        /// </param>
        protected ContextBase(DbConnection existingConnection)
            : base(existingConnection, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextBase"/> class.
        /// </summary>
        /// <param name="existingConnection">
        /// The existing connection.
        /// </param>
        /// <param name="contextOwnsConnection">
        /// The context owns connection.
        /// </param>
        protected ContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder that defines the model for the context being created.
        /// </param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///     classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}