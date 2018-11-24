// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEndPointParamMap.cs" company="Block Bonds">
//   Copyright © 2017. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System;

    /// <summary>
    ///     The EndPointParamMap interface.
    /// </summary>
    public interface IEndPointParamMap
    {
        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        //string EndPoint { get; set; }
        
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        IParameter Parameter { get; set; }
    }
}