// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity.Migrations;

namespace CardValidation.Repository
{
    /// <summary>
    ///     The configuration.
    /// </summary>
    public class Configuration : DbMigrationsConfiguration<CardValidationContext>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(CardValidationContext context)
        {
            //DefaultData.Seed(context);
        }

        #endregion
    }
}