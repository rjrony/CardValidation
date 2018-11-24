// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiHeaderParameterMap.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     The ApiHeaderParameterMap interface.
    /// </summary>
    public interface IApiHeaderParameterMap
    {       
        /// <summary>
        /// Builds the endpoints.
        /// </summary>
        /// <returns>List of EndPointParameters</returns>
        List<IEndPointParameters> BuildEndpoints();
    }
}