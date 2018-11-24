// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Extensions
{
    using System;
    using System.Collections;
    using System.Linq;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class WebExtensions
    {
        /// <summary>
        /// The to query string.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// ex
        /// </exception>
        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            // Get all properties on the object
            var properties =
                request.GetType()
                    .GetProperties()
                    .Where(x => x.CanRead)
                    .Where(x => x.GetValue(request, null) != null)
                    .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties.Where(x => !(x.Value is string) && x.Value is IEnumerable).Select(x => x.Key).ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType ? valueType.GetGenericArguments()[0] : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join(
                "&",
                properties.Select(x => string.Concat(Uri.EscapeDataString(x.Key.ToLower()), "=", Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}