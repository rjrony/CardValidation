// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceCode.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization
{
    using System;
    using System.Globalization;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The resource code.
    /// </summary>
    public class ResourceCode : ILocalizationResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCode"/> class.
        /// </summary>
        /// <param name="resourceKey">
        /// The resource key.
        /// </param>
        /// <param name="resourceType">
        /// The resource type.
        /// </param>
        public ResourceCode(string resourceKey, Type resourceType)
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
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// not implemented now
        /// </exception>
        public string GetString(object[] arguments)
        {
            throw new NotImplementedException();
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