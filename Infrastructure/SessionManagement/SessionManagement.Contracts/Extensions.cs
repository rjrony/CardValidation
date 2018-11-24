// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.SessionManagement.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The get single header.
        /// </summary>
        /// <param name="requestInfo">
        /// The request info.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="nullException">
        /// The null Exception.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetSingleHeader(this IRequestInfo requestInfo, string key, bool nullException = false)
        {
            IEnumerable<string> list = requestInfo.Headers.SingleOrDefault(x => x.Key == key).Value;
            string value = null;
            if (list != null)
            {
                value = list.FirstOrDefault();
            }

            if (string.IsNullOrEmpty(value) && nullException)
            {
                throw new Exception($"{key} is not available in header");
            }

            return value;
        }
    }
}