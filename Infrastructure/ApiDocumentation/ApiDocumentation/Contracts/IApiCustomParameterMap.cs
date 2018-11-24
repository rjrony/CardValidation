// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiCustomParameterMap.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     The EndPointCustomParamMap interface.
    /// </summary>
    public interface IApiCustomParameterMap
    {
        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        string EndPoint { get; set; }

        /// <summary>
        ///     Gets or sets the group
        /// </summary>
        List<string> GroupList { get; set; }

        /// <summary>
        ///     Gets or sets the parameter type.
        /// </summary>
        List<IParameter> Parameters { get; set; }
    }
}