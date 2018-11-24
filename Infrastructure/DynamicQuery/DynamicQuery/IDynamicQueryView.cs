// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDynamicQueryView.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    /// <summary>
    /// The DynamicQueryView interface.
    /// </summary>
    /// <typeparam name="T">
    /// generic
    /// </typeparam>
    public interface IDynamicQueryView<in T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        int Total { get; set; }
    }
}