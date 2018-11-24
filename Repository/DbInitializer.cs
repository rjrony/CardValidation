// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbInitializer.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The bb db initializer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception.Contract;
using Infrastructure.Repository;

namespace CardValidation.Repository
{
    /// <summary>
    ///     The bb db initializer.
    /// </summary>
    public class DbInitializer : DbInitializerBase<CardValidationContext, Configuration>
    {
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbInitializer"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        public DbInitializer(IDependencyResolver dependencyResolver): base(dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        #region Public Methods and Operators

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void Seed(CardValidationContext context)
        {
            var defaultData = (DefaultData)this.dependencyResolver.Resolve(typeof(DefaultData));
            defaultData.Seed(context);

            //base.Seed(context);
        }

        /// <summary>
        ///     The get full text catalog name.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        protected override string GetFullTextCatalogName()
        {
            return "PRODUCT_CATALOG_FULLTEXT";
        }

        #endregion
    }
}