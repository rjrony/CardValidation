// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEndPointParameters.cs" company="">
//   Copyright © 2017. All rights reserved.
// </copyright>
// <summary>
//   Defines the IEndPointParameters type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// The EndPointParameters interface.
    /// </summary>
    public interface IEndPointParameters
    {
        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        string ApiEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the end point parameter maps.
        /// </summary>
        /// <value>
        /// The end point parameter maps.
        /// </value>
        List<IEndPointParamMap> EndPointParameters { get; set; }
    }
}