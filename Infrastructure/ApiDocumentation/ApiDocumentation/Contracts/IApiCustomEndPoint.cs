// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiCustomEndPoint.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     The ApiCustomEndPoint interface.
    /// </summary>
    public interface IApiCustomEndPoint
    {
        /// <summary>
        ///     The custom end points.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDictionary" />.
        /// </returns>
        List<IApiCustomParameterMap> CustomEndPoints();
    }
}