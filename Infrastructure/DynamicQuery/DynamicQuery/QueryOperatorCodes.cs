// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryOperatorCodes.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    /// <summary>
    ///     The query operator codes.
    /// </summary>
    public enum QueryOperatorCodes
    {
        /// <summary>
        ///     The equal.
        /// </summary>
        Equal = 1,

        /// <summary>
        ///     The greater than.
        /// </summary>
        GreaterThan,

        /// <summary>
        ///     The less than.
        /// </summary>
        LessThan,

        /// <summary>
        ///     The greater than or equal.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        ///     The less than or equal.
        /// </summary>
        LessThanOrEqual
    }

    /// <summary>
    ///     The query logic codes.
    /// </summary>
    public enum QueryLogicCodes
    {
        /// <summary>
        ///     The or.
        /// </summary>
        Or = 1,

        /// <summary>
        ///     The and.
        /// </summary>
        And
    }
}