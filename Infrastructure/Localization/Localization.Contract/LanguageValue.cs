// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LanguageValue.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization.Contracts
{
    /// <summary>
    ///     The language value.
    /// </summary>
    public class LanguageValue
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the language.
        /// </summary>
        public ILanguage Language { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        #endregion
    }
}