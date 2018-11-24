// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterEnumeration.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    using System;

    

    /// <summary>
    /// The filter enumeration.
    /// </summary>
    /// <typeparam name="T">
    /// generic
    /// </typeparam>
    public class FilterEnumeration<T> : Enumeration<T>
        where T : IComparable<T>, new()
    {
        /// <summary>
        ///     Gets or sets the operator.
        /// </summary>
        public QueryOperatorCodes Operator { get; set; }
    }
}