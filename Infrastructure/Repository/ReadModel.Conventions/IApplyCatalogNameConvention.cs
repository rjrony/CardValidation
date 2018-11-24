// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplyCatalogNameConvention.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel.Conventions
{
    /// <summary>
    ///     Implement this interface if you want to apply a custom naming convention to the default catalog name.
    /// </summary>
    public interface IApplyCatalogNameConvention
    {
        /// <summary>
        /// Applies the convention.
        /// </summary>
        /// <param name="inCatalogName">
        /// Name of the catalog.
        /// </param>
        /// <param name="dbCatalogPrefix">
        /// The database catalog prefix.
        /// </param>
        /// <returns>
        /// The modified Catalog Name.
        /// </returns>
        string Apply(string inCatalogName, string dbCatalogPrefix);
    }
}