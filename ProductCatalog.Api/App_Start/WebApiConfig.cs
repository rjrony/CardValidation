using System.Web.Http;
using CardValidation.Api.Controllers;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using Microsoft.Web.Http.Versioning.Conventions;
using Newtonsoft.Json.Serialization;

namespace CardValidation.Api
{
    /// <summary>
    /// web api config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.AddApiVersioning(
                options =>
                    {
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.DefaultApiVersion = new ApiVersion(ApiVersionSettings.DefaultMajorVersion, ApiVersionSettings.DefaultMinorVersion);
                        options.ApiVersionReader = new HeaderApiVersionReader(ApiVersionSettings.ApiVersionHeaderName);
                        options.Conventions = GetApiVersionConventionBuilder();
                    });

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //      json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        /// <summary>
        /// The get api version convention builder.
        /// </summary>
        /// <returns>
        /// The <see cref="ApiVersionConventionBuilder"/>.
        /// </returns>
        public static ApiVersionConventionBuilder GetApiVersionConventionBuilder()
        {
            var conv = new ApiVersionConventionBuilder();

            conv.Controller<TestController>().HasApiVersion(ApiVersionSettings.CurrentMajorVersion, ApiVersionSettings.CurrentMinorVersion)
                .HasApiVersion(ApiVersionSettings.DefaultMajorVersion, ApiVersionSettings.DefaultMinorVersion)
                .HasDeprecatedApiVersion(ApiVersionSettings.DepreciatedMajorVersion, ApiVersionSettings.DepreciatedMinorVersion);


            return conv;
        }
    }
}
