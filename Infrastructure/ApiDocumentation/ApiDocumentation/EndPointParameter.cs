// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndPointParameter.cs" company="">
//   Copyright © 2017. All rights reserved.
// </copyright>
// <summary>
//   The end point parameter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation
{
    using Infrastructure.ApiDocumentation.Contracts;

    /// <summary>
    /// The end point parameter.
    /// </summary>
    public class EndPointParameter : IEndPointParamMap
    {
        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        //public string EndPoint { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        public IParameter Parameter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is optional; otherwise, <c>false</c>.
        /// </value>
        public bool IsOptional { get; set; }
    }
}
