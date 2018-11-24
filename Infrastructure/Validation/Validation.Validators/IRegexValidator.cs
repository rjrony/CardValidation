// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRegexValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    /// <summary>
    ///     Interface for regex based validators.
    /// </summary>
    public interface IRegexValidator
    {
        /// <summary>
        ///     Gets the regex pattern.
        /// </summary>
        /// <value>
        ///     The regex pattern.
        /// </value>
        string RegexPattern { get; }
    }
}