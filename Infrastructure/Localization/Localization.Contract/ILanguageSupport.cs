// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILanguageSupport.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     The LanguageSupport interface.
    /// </summary>
    public interface ILanguageSupport
    {
        /// <summary>
        ///     The get all languages.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<ILanguage> GetAllLanguages();

        /// <summary>
        ///     The get current language.
        /// </summary>
        /// <returns>
        ///     The <see cref="Localization.Language" />.
        /// </returns>
        ILanguage GetCurrentLanguage();

        /// <summary>
        ///     The get tenant languages.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<ILanguage> GetTenantLanguages();

        /// <summary>
        ///     The get user languages.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<ILanguage> GetUserLanguages();
    }
}