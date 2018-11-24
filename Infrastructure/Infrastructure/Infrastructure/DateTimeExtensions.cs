// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     The date time extention.
    /// </summary>
    public static class DateTimeExtention
    {
        /// <summary>
        ///     The date format.
        /// </summary>
        private const string DateFormat = "dd.MM.yyyy";

        /// <summary>
        ///     The date time format.
        /// </summary>
        private const string DateTimeFormat = "dd.MM.yyyy H:mm tt";

        /// <summary>
        /// To the date only.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The only Date part of this DateTime.
        /// </returns>
        public static DateTime ToDateOnly(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// To the date only.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// Retuns DateTime.MinValue() if DateTime is null otherwise the Date part of this DateTime.
        /// </returns>
        public static DateTime? ToDateOnly(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return DateTime.MinValue.Date;
            }

            return date.Value.Date;
        }

        /// <summary>
        /// The to date string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateString() : string.Empty;
        }

        /// <summary>
        /// The to date string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateString(this DateTime date)
        {
            return date.ToString(DateFormat);
        }

        /// <summary>
        /// The to date time.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>
        public static DateTime? ToDateTime(this string date)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(date, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }

            return null;
        }

        /// <summary>
        /// The to date time.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>
        public static DateTime? ToDateTime(this string date, string format)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }

            return null;
        }

        /// <summary>
        /// The to date time string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateString() : string.Empty;
        }

        /// <summary>
        /// The to date time string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime? date, string format)
        {
            return string.IsNullOrEmpty(format) ? date.ToDateString() : date.HasValue ? date.Value.ToString(format) : string.Empty;
        }

        /// <summary>
        /// The to date time string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime date, string format)
        {
            return string.IsNullOrEmpty(format) ? date.ToDateString() : date.ToString(format);
        }

        /// <summary>
        /// The to date time string.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString(DateTimeFormat);
        }
    }
}