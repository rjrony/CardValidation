// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceProvider.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    using System.Collections.Generic;
    using Westwind.Globalization;

    /// <summary>
    ///     The resource provider.
    /// </summary>
    public class ResourceProvider
    {
        /// <summary>
        /// Gets or sets the resource set.
        /// </summary>
        public static string ResourceSet { get; set; }

        /// <summary>
        /// The get all resource.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<ResourceIdItem> GetAllResource()
        {
            var dataManager = new DbResourceSqlServerDataManager();
            var list = dataManager.GetAllResourceIds(ResourceSet);
            return list;
        }

        /// <summary>
        /// The get resource.
        /// </summary>
        /// <param name="resourceId">
        /// The resource id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetResource(string resourceId)
        {
            return DbRes.T(resourceId, ResourceSet);
        }

        /// <summary>
        /// The write resource.
        /// </summary>
        /// <param name="resourceId">
        /// The resource id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool WriteResource(string resourceId, string value)
        {
            return DbRes.WriteResource(resourceId, value, null, ResourceSet);
        }
    }
}