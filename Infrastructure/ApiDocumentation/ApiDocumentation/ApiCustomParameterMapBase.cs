// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiCustomParameterMapBase.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation
{
    using System.Collections.Generic;

    using Infrastructure.ApiDocumentation.Contracts;

    /// <summary>
    ///     The api custom parameter map base.
    /// </summary>
    public abstract class ApiCustomParameterMapBase : IApiCustomEndPoint
    {
        /// <summary>
        ///     The custom end points.
        /// </summary>
        /// <returns>
        ///     The <see cref="List" />.
        /// </returns>
        public abstract List<IApiCustomParameterMap> CustomEndPoints();
    }
}