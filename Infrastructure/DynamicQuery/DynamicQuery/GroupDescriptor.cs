// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupDescriptor.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.DynamicQuery
{
    /// <summary>
    ///     Group Descriptor
    /// </summary>
    public class GroupDescriptor
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Method to return expression
        /// </summary>
        /// <returns>returns expression</returns>
        public string ToExpression()
        {
            return this.Field;
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