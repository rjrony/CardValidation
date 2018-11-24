// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Exception.WebApiBasicConsole;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Infrastructure.Exception.WebApiBasicConsole
{
    using System.Web.Http;

    using Owin;

    /// <summary>
    ///     The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            // config.Filters.Add(new NotImplExceptionFilterAttribute());
            app.UseWebApi(config);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}