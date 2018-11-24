// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocalizationResource.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization.Contracts
{
    using System.Globalization;

    /// <summary>
    ///     The LocalizationResource interface.
    /// </summary>
    public interface ILocalizationResource
    {
        /// <summary>
        ///     Gets the key.
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     Gets the resource type full name.
        /// </summary>
        string ResourceTypeFullName { get; }

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetString(object[] arguments);

        /// <summary>
        /// The get string by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetStringByKey(string key);

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
        string GetStringByKey(string key, CultureInfo targetCulture);
    }
}