// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseFilter.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BaseFilter<T> : BaseFilter
    {
        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        public T Value { get; set; }

        //public List<BaseFilter<TList>> Filters { get; set; }            
    }

    /// <summary>
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// </summary>
        public QueryOperatorCodes Operator { get; set; }
    }
}