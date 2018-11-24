// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicQueryData.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Grid Response Result
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class DynamicQueryData<T>
        where T : class
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets Data info
        /// </summary>
        public IQueryable<T> Data { get; set; }

        /// <summary>
        ///     Gets or sets GroupCount info
        /// </summary>
        public IList<int> GroupCount { get; set; }

        /// <summary>
        ///     Gets or sets Total info
        /// </summary>
        public int Total { get; set; }

        #endregion
    }
}