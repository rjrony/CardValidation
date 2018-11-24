// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestStartup.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using Infrastructure.Host;
using Infrastructure.Host.Contracts;
using Infrastructure.Interception.Contract;
using Infrastructure.Testing.WebApi;
using Owin;

namespace CardValidation.Api.IntegrationTests
{
    /// <summary>
    ///     The test startup.
    /// </summary>
    public class TestStartup : Startup, ITestStartup
    {
        /// <summary>
        ///     Gets or sets the bootstrapper web api.
        /// </summary>
        public static BootstrapperWebApi BootstrapperWebApi { get; set; }

        /// <summary>
        ///     Gets or sets the http configuration.
        /// </summary>
        public static HttpConfiguration HttpConfiguration { get; set; }

        /// <summary>
        ///     The configuration.
        /// </summary>
        /// <param name="app">
        ///     The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            //app.Use<FakeClaimReaderMiddleware>();

            base.Configuration(app);
        }

        /// <summary>
        ///     The configuration extension.
        /// </summary>
        /// <param name="config">
        ///     The config.
        /// </param>
        /// <param name="dependencyResolver">
        ///     The dependency Resolver.
        /// </param>
        public override void ConfigurationExtension(HttpConfiguration config, IDependencyResolver dependencyResolver)
        {
            
            return;
        }

        /// <summary>
        ///     The get bootstrapper web api.
        /// </summary>
        /// <param name="config">
        ///     The config.
        /// </param>
        /// <returns>
        ///     The <see cref="IBootstrapper" />.
        /// </returns>
        public override IBootstrapper GetBootstrapperWebApi(HttpConfiguration config)
        {
            BootstrapperWebApi = new BootstrapperWebApi(config);
            return BootstrapperWebApi;
        }

        /// <summary>
        ///     The get injection configuration.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpConfiguration" />.
        /// </returns>
        public override HttpConfiguration GetInjectionConfiguration()
        {
            HttpConfiguration = new HttpConfiguration();
            return HttpConfiguration;
        }
    }
}