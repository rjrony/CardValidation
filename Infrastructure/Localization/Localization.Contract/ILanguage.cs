// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILanguage.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Localization.Contracts
{
    using System.Globalization;

    /// <summary>
    /// The Language interface.
    /// </summary>
    public interface ILanguage
    {
        /// <summary>
        /// Gets or sets the culture info.
        /// </summary>
        CultureInfo CultureInfo { get; set; }
    }
}