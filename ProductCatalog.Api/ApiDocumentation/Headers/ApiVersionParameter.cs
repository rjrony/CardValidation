// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiVersionParameter.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The api version parameter header parameter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.ApiDocumentation.Contracts;

namespace CardValidation.Api.ApiDocumentation.Headers
{
    /// <summary>
    ///     The api version header parameter.
    /// </summary>
    public class ApiVersionParameter : HeaderParameterBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiVersionParameter" /> class.
        /// </summary>
        public ApiVersionParameter()
        {
            this.Name = "api-version";
            this.In = "header";
            this.Type = "string";
            this.Required = true;
            this.Description = "api version number";
            this.IsDefault = false;
        }

        private static ApiVersionParameter instance;

        public static ApiVersionParameter GetInstance()
        {
            if (instance == null)
            {
                instance = new ApiVersionParameter();
            }

            return instance;
        }
    }
}
