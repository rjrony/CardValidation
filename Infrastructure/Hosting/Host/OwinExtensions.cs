// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwinExtensions.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Owin;

    /// <summary>
    /// The owin extensions.
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// The use unity web api.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <returns>
        /// The <see cref="IAppBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IAppBuilder UseUnityWebApi(this IAppBuilder app, HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (!configuration.MessageHandlers.OfType<DependencyScopeHandler>().Any())
            {
                configuration.MessageHandlers.Insert(0, new DependencyScopeHandler());
            }

            return app;
        }
    }
}