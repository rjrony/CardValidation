// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwinExtensions.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Infrastructure.DependencyContainerBuilder;
using Infrastructure.Interception.Contract;
using Microsoft.Owin;
using Owin;

namespace Host.Owin
{
// using DependencyContainerBuilder;

    /// <summary>
    ///     The owin extensions.
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// The get dependency resolver.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IDependencyResolver"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IDependencyResolver GetDependencyResolver(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return
                context.Get<ScopedApplicationDependecyResolver>(OwinKeyConstants.ScopedDependencyResolver)
                    .Resolve<IDependencyResolver>();
        }

        /// <summary>
        /// The get scoped application dependecy resolver.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Infrastructure.DependencyContainerBuilder.ApplicationDependecyResolver"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static ScopedApplicationDependecyResolver GetScopedApplicationDependecyResolver(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.Get<ScopedApplicationDependecyResolver>(OwinKeyConstants.ScopedDependencyResolver);
        }

        /// <summary>
        /// The use unity middleware.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// The <see cref="IAppBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IAppBuilder UseDependencyInjectorMiddleware(this IAppBuilder app, 
            ApplicationDependecyResolver container)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            // idsvr : remove these guards so that multiple copies of middleware can be registered
            //if (app.Properties.ContainsKey(MiddlewareRegisteredKey)) return app;
            app.Use(async (context, next) =>
            {
                using (var dependencyScope = container.BeginScope())
                {
                    context.Set(OwinKeyConstants.ScopedDependencyResolver, dependencyScope);

//    context.Set(OwinKeyConstants.IdependencyResolver, dependencyScope.GetService(typeof(Interception.Contract.IDependencyResolver)));
                    await next();
                }
            });


            //UseMiddlewareFromContainer(app, container);

            // idsvr : remove these guards so that multiple copies of middleware can be registered
            //app.Properties.Add(MiddlewareRegisteredKey, true);
            return app;
        }
    }
}