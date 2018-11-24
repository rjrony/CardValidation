// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationResource.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization
{
    using System;
    using System.Globalization;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The localization resource.
    /// </summary>
    public class LocalizationResource : ILocalizationResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationResource"/> class.
        /// </summary>
        /// <param name="resourceKey">
        /// The resource key.
        /// </param>
        /// <param name="resourceType">
        /// The resource type.
        /// </param>
        public LocalizationResource(string resourceKey, Type resourceType)
        {
            this.Key = resourceKey;
            this.ResourceTypeFullName = resourceType.FullName;
        }

        /// <summary>
        ///     Gets the key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Gets the resource type full name.
        /// </summary>
        public string ResourceTypeFullName { get; }

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.cv
        /// </returns>
        public string GetString(object[] arguments)
        {
            /*
            if (resource == null)
            {
                return string.Empty;
            }

            return new ResourceTranslator().GetString(resource, arguments);
            */
            return string.Empty;
        }

        /// <summary>
        /// The get string by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string GetStringByKey(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get string by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="targetCulture">
        /// The target culture.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string GetStringByKey(string key, CultureInfo targetCulture)
        {
            throw new NotImplementedException();
        }
    }
}