// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    /// <summary>
    ///     Extension methods for string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Formats a string with the specified values.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string FormatWith(this string format, params object[] values)
        {
            return string.Format(format, values);
        }
    }
}