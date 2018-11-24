// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadModelNameConvention.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ReadModel
{
    using Infrastructure.ReadModel.Conventions;

    /// <summary>
    ///     Name Convention for Cqrs ReadModels.
    /// </summary>
    public class ReadModelNameConvention : IApplyCatalogNameConvention
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
            string outCatalogName;

            if (inCatalogName.Contains("Context"))
            {
                outCatalogName = inCatalogName.Replace("Context", "ReadModel");
            }
            else
            {
                outCatalogName = inCatalogName + "ReadModel";
            }

            outCatalogName = outCatalogName.Replace("Db", string.Empty);
            outCatalogName = outCatalogName.Replace("DB", string.Empty);

            return dbCatalogPrefix == null ? "Pw_{0}".FormatWith(outCatalogName) : "Pw_{0}_{1}".FormatWith(dbCatalogPrefix, outCatalogName);
        }
    }
}