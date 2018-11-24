// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiHeaderParameterMappings.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The api header parameter map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CardValidation.Api.ApiDocumentation.Headers;
using Infrastructure.ApiDocumentation;
using Infrastructure.ApiDocumentation.Contracts;
using Infrastructure.Logging.Contracts;

namespace CardValidation.Api.ApiDocumentation
{
    /// <summary>
    ///     The api header parameter map.
    /// </summary>
    public class ApiHeaderParameterMappings : ApiHeaderParameterMapBase
    {
        /// <summary>
        /// The end points
        /// </summary>
        private static List<EndPoint> endPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHeaderParameterMappings"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ApiHeaderParameterMappings(ILogger logger)
            : base(logger)
        {
            endPoints = new List<EndPoint>();
        }

        /// <summary>
        ///     The get api end point type.
        /// </summary>
        /// <returns>
        ///     The <see cref="Type" />.
        /// </returns>
        public override Type GetApiEndPointType()
        {
            return typeof(ApiPaths);
        }

        /// <summary>
        /// The get custom endpoint parameters.
        /// </summary>
        /// <returns>
        /// The <see cref="!:List" />.
        /// </returns>
        public override List<EndPoint> GetCustomEndpoints()
        {
            this.Logger.Debug(() => "Preparing Custom Endpoints");
            var airtimeTopup = new EndPoint
            {
                ApiEndPoint = ApiPaths.TestGet,
                EndPointParameters =new List<IEndPointParamMap>()
                                           {
                                               new EndPointParameter
                                                       {
                                                           Parameter= ApiVersionParameter.GetInstance(),
                                                           IsActive = true
                                                       }
                                           }
            };
            endPoints.Add(airtimeTopup);
            return endPoints;
        }
    }
}