// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationSupport.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     LocalizationSupport class
    /// </summary>
    public class LocalizationSupport : ILocalizationSupport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationSupport"/> class.
        /// </summary>
        /// <param name="languageSupport">
        /// The language support.
        /// </param>
        public LocalizationSupport(ILanguageSupport languageSupport)
        {
            this.LanguageSupport = languageSupport;
        }

        /// <summary>
        ///     Gets or sets the language support.
        /// </summary>
        /// <value>
        ///     The language support.
        /// </value>
        public ILanguageSupport LanguageSupport { get; set; }

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
        public IList<LanguageValue> GetLanguageValues(object entity, string propertyName, string columnSeperator)
        {
            var translations = new List<LanguageValue>();
            if (entity != null)
            {
                var defaultProperty =
                    entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance).GetValue(entity);

                foreach (var lstLanguage in this.LanguageSupport.GetAllLanguages())
                {
                    LanguageValue languageValue;
                    var languageName = this.ToPascalCase(lstLanguage.CultureInfo.TwoLetterISOLanguageName);
                    string readMember = this.GetTheMultiLanguagePropertyName(propertyName, columnSeperator, languageName);
                    var setterProperty = entity.GetType().GetProperty(readMember, BindingFlags.Public | BindingFlags.Instance);
                    if (setterProperty != null)
                    {
                        var setterPropertyValue = setterProperty.GetValue(entity);
                        languageValue = new LanguageValue { Language = lstLanguage, Value = setterPropertyValue.ToString() };
                    }
                    else
                    {
                        languageValue = new LanguageValue
                                            {
                                                Language = lstLanguage,
                                                Value = defaultProperty != null ? defaultProperty.ToString() : null
                                            };
                    }

                    translations.Add(languageValue);
                }
            }

            return translations;
        }

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
        public void SetLanguageValue(
            IEnumerable<LanguageValue> languageValues,
            object entity,
            string propertyName,
            string columnSeperator = "_")
        {
            if (entity == null)
            {
                throw new Exception("Entity fields cannot be null");
            }

            var languageProperty = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (languageProperty != null)
            {
                foreach (var languageValue in languageValues)
                {
                    var languageName = this.ToPascalCase(languageValue.Language.CultureInfo.TwoLetterISOLanguageName);
                    var readMember = this.GetTheMultiLanguagePropertyName(propertyName, columnSeperator, languageName);
                    var propertyToBeFilled = entity.GetType().GetProperty(readMember, BindingFlags.Public | BindingFlags.Instance);
                    if (propertyToBeFilled != null)
                    {
                        propertyToBeFilled.SetValue(entity, languageValue.Value);
                    }
                }
            }
        }

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
        public void SetLocalizeValue(
            object entity,
            string propertyName,
            object localizeEntity,
            string localizePropertyName,
            string cultureNameSeperator = "")
        {
            if (entity != null && localizeEntity != null)
            {
                foreach (var lstLanguage in this.LanguageSupport.GetAllLanguages())
                {
                    var languageName = this.ToPascalCase(lstLanguage.CultureInfo.TwoLetterISOLanguageName);
                    var readMember = this.GetTheMultiLanguagePropertyName(propertyName, "_", languageName);
                    var writeMember = this.GetTheMultiLanguagePropertyName(localizePropertyName, cultureNameSeperator, languageName);

                    this.SetValue(entity, readMember, localizeEntity, writeMember);
                }
            }
        }

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
        public void SetNonLocalizeValue(object entity, string propertyName, object localizeEntity, string localizePropertyName)
        {
            if (entity != null && localizeEntity != null)
            {
                this.SetValue(entity, propertyName, localizeEntity, localizePropertyName);
            }
        }

        /// <summary>
        /// Gets the name of the multi language property.
        /// </summary>
        /// <param name="theString">
        /// The string.
        /// </param>
        /// <param name="cultureNameSeperator">
        /// The culture name seperator.
        /// </param>
        /// <param name="languageName">
        /// Name of the language.
        /// </param>
        /// <returns>
        /// string
        /// </returns>
        private string GetTheMultiLanguagePropertyName(string theString, string cultureNameSeperator, string languageName)
        {
            return theString + cultureNameSeperator + languageName;
        }

        /// <summary>
        /// Sets the value.
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
        private void SetValue(object entity, string propertyName, object localizeEntity, string localizePropertyName)
        {
            var propertyInfo = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                var setterProperty = localizeEntity.GetType().GetProperty(localizePropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (setterProperty != null)
                {
                    var setterPropertyValue = setterProperty.GetValue(localizeEntity);
                    propertyInfo.SetValue(entity, setterPropertyValue);
                }
            }
        }

        /// <summary>
        /// To the camel case.
        /// </summary>
        /// <param name="theString">
        /// The string.
        /// </param>
        /// <returns>
        /// string
        /// </returns>
        private string ToPascalCase(string theString)
        {
            // If there are 0 or 1 characters, just return the string.
            if (theString == null)
            {
                return null;
            }

            if (theString.Length < 2)
            {
                return theString.ToUpper();
            }

            // Split the string into words.
            string[] words = theString.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = string.Empty;
            foreach (string word in words)
            {
                result += word.Substring(0, 1).ToUpper() + word.Substring(1);
            }

            return result;
        }
    }
}