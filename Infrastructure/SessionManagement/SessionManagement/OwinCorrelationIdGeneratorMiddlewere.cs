// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwinCorrelationIdGeneratorMiddlewere.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Host.Owin;
using Infrastructure.Interception.Contract;
using Infrastructure.SessionManagement.Contracts;
using Microsoft.Owin;
using Owin;

namespace Infrastructure.SessionManagement
{
    internal class OwinCorrelationIdGeneratorMiddlewere : OwinMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwinCorrelationIdGeneratorMiddlewere"/> class.
        /// </summary>
        /// <param name="next">
        /// The next.
        /// </param>
        public OwinCorrelationIdGeneratorMiddlewere(OwinMiddleware next) : base(next)
        {
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task Invoke(IOwinContext context)
        {

            var dependencyResolver = context.GetDependencyResolver().Resolve<IDependencyResolver>();
            var operationContext = dependencyResolver.Resolve<IRequestInfo>() as IOperationContext;

            var correlationIdFromRequestHeader = context.Request.Headers["CorrelationId"];
            string correlationId = string.Empty;
            if (!string.IsNullOrEmpty(correlationIdFromRequestHeader))
            {
                correlationId = correlationIdFromRequestHeader;
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
            }
            operationContext.CorrelationId = correlationId;

            dependencyResolver.RegisterInstacnceAsUnityOfWork(operationContext);

            var response = context.Response;

            response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;

                if ((resp.ContentLength != null || resp.StatusCode!=200) && resp.Headers.Get("CorrelationId") == null)
                {
                    resp.Headers.Add("CorrelationId", new[] { correlationId });
                }

            }, response);

            await this.Next.Invoke(context);
        }
    }


    /// <summary>
    ///     The owin correlation id generator middlewere app builder extensions.
    /// </summary>
    public static class OwinCorrelationIdGeneratorMiddlewereAppBuilderExtensions
    {
        /// <summary>
        /// The use owin exception handler.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public static void UseCorrelationIdInjector(this IAppBuilder app)
        {
            app.Use(typeof(OwinCorrelationIdGeneratorMiddlewere));
        }
    }
}