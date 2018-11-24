// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicQueryView.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.DynamicQuery
{
    using System.Collections.Generic;

    /// <summary>
    /// The dynamic query view.
    /// </summary>
    /// <typeparam name="T">
    /// generic
    /// </typeparam>
    public class DynamicQueryView<T> : IDynamicQueryView<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public int Total { get; set; }
    }
}