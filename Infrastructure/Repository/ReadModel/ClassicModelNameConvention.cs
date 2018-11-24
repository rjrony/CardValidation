// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassicModelNameConvention.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using Infrastructure.ReadModel.Conventions;

    /// <summary>
    ///     Name convention for standard databases.
    /// </summary>
    public class ClassicModelNameConvention : IApplyCatalogNameConvention
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
        public string Apply(string inCatalogName, string dbCatalogPrefix)
        {
            var outCatalogName = inCatalogName.Replace("Context", string.Empty);
            outCatalogName = outCatalogName.Replace("Db", string.Empty);
            outCatalogName = outCatalogName.Replace("DB", string.Empty);

            return dbCatalogPrefix == null ? "bb_{0}".FormatWith(outCatalogName) : "bb_{0}_{1}".FormatWith(dbCatalogPrefix, outCatalogName);
        }
    }
}