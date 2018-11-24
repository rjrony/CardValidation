// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiExceptionConfiguration.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   Defines the ApiExceptionConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Exception;

namespace CardValidation.Api
{
    internal class ApiExceptionConfiguration : IExceptionConfiguration
    {
        public bool ShowNestedMessage()
        {
            // don't show nested message in UAT & PROD. uncomment below line after mobile application is independent of nested messages.
            //return EnvironmentInfo.EnvironmentName.ToLowerInvariant() != EnvironmentName.Prod & EnvironmentInfo.EnvironmentName.ToLowerInvariant() != EnvironmentName.Uat;
            return true;
        }
    }
}