// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterDescriptor.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Filter Descriptor
    /// </summary>
    public class FilterDescriptor
    {
        #region Static Fields

        /// <summary>
        ///     Operators list
        /// </summary>
        private static readonly IDictionary<string, string> Operators = new Dictionary<string, string> { { "eq", "=" } };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic LINQ e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">
        /// A list of flattened filters
        /// </param>
        /// <returns>
        /// returns expression
        /// </returns>
        public string ToExpression(IList<FilterDescriptor> filters)
        {
            if (this.Filters != null && this.Filters.Any())
            {
                return "(" + string.Join(" " + this.Logic + " ", this.Filters.Select(filter => filter.ToExpression(filters)).ToArray())
                       + ")";
            }

            return string.Format("{0} {1} @{2}", this.Field, Operators[this.Operator], filters.IndexOf(this));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets Field info
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     Gets or sets Filter info
        /// </summary>
        public IList<FilterDescriptor> Filters { get; set; }

        /// <summary>
        ///     Gets or sets Logic info
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        ///     Gets or sets Operator info
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        ///     Gets or sets Value info
        /// </summary>
        public object Value { get; set; }

        #endregion
    }
}