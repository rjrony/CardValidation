// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocalizationSupport.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     ILocalizationSupport interface
    /// </summary>
    public interface ILocalizationSupport
    {
        /// <summary>
        ///     Gets or sets the language support.
        /// </summary>
        /// <value>
        ///     The language support.
        /// </value>
        ILanguageSupport LanguageSupport { get; set; }

        /// <summary>
        /// Gets the language values.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="columnSeperator">
        /// The column seperator.
        /// </param>
        /// <returns>
        /// IList LanguageValue
        /// </returns>
        IList<LanguageValue> GetLanguageValues(object entity, string propertyName = "Text", string columnSeperator = "");

        /// <summary>
        /// Sets the dynamic language value.
        /// </summary>
        /// <param name="languageValues">
        /// The language values.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="columnSeperator">
        /// The column seperator.
        /// </param>
        /// <exception cref="System.Exception">
        /// Entity fields cannot be null
        /// </exception>
        void SetLanguageValue(IEnumerable<LanguageValue> languageValues, object entity, string propertyName, string columnSeperator = "_");

        /// <summary>
        /// Sets the localize value.
        /// </summary>
        /// <param name="entity">
        /// The entity to be filled.
        /// </param>
        /// <param name="propertyName">
        /// The read mulitlanguage column prefix.
        /// </param>
        /// <param name="localizeEntity">
        /// The writer entity.
        /// </param>
        /// <param name="localizePropertyName">
        /// The setter column prefix.
        /// </param>
        /// <param name="cultureNameSeperator">
        /// The culture name seperator.
        /// </param>
        void SetLocalizeValue(
            object entity,
            string propertyName,
            object localizeEntity,
            string localizePropertyName,
            string cultureNameSeperator = "");

        /// <summary>
        /// Sets the non localize value.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="localizeEntity">
        /// The localize entity.
        /// </param>
        /// <param name="localizePropertyName">
        /// Name of the localize property.
        /// </param>
        void SetNonLocalizeValue(object entity, string propertyName, object localizeEntity, string localizePropertyName);
    }
}