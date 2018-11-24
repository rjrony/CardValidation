// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortDescriptor.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    using System.Collections.Generic;

    /// <summary>
    ///     Sort Descriptor
    /// </summary>
    public class SortDescriptor
    {
        #region Static Fields

        /// <summary>
        ///     Operators list
        /// </summary>
        private static readonly IDictionary<string, string> Operators = new Dictionary<string, string>
                                                                            {
                                                                                { "asc", "Ascending" },
                                                                                { "desc", "Descending" }
                                                                            };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Method to return expression
        /// </summary>
        /// <returns>return expression</returns>
        public string ToExpression()
        {
            return this.Field + " " + Operators[this.Dir];
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets info
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        ///     Gets or sets Field info
        /// </summary>
        public string Field { get; set; }

        #endregion
    }
}