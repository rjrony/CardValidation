// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Testing.Test
{
    using System.Collections;
    using System.Linq;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The first.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object First(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }

            var list = enumerable as IList;
            if (list != null)
            {
                if (list.Count == 0)
                {
                    return null;
                }

                return list[0];
            }

            return enumerable.Cast<object>().FirstOrDefault();
        }

        #endregion
    }
}