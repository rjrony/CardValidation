// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OwinExceptionHandlerMiddleware.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Host.Owin;
using Infrastructure.Exception.ErrorCodes;
using Infrastructure.Logging.Contracts;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

namespace Infrastructure.Exception
{
    using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    /// <summary>
    ///     The owin exception handler middleware.
    /// </summary>
    /// <summary>
    ///     The owin exception handler middleware.
    /// </summary>
    public class OwinExceptionHandlerMiddleware
    {
        private readonly AppFunc _next;

        private readonly IExceptionConfiguration _exceptionConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next.
        /// </param>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public OwinExceptionHandlerMiddleware(AppFunc next)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            this._next = next;

            this._exceptionConfiguration = new DefaultExceptionConfiguration();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next.
        /// </param>
        /// <param name="exceptionConfiguration">
        /// The exception configuration.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public OwinExceptionHandlerMiddleware(AppFunc next, IExceptionConfiguration exceptionConfiguration)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            this._next = next;

            this._exceptionConfiguration = exceptionConfiguration;
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {
                await this._next(environment);
            }
            catch (System.Exception ex)
            {
                var owinContext = new OwinContext(environment);

                this.HandleException(ex, owinContext);

                return;
            }
        }

        private void HandleException(System.Exception ex, IOwinContext context)
        {
            string exceptionMessage = "";
            AppException exception = null;
            var logger = context.GetDependencyResolver().Resolve<ILogger>();

            bool showNestedMessage = this._exceptionConfiguration.ShowNestedMessage();

            if (ex is AppException)
            {
                exception = (AppException)ex;
                //    exceptionMessage = $"An handled exception occured with StatusCode {exception.HttpStatusCode}";
                

                if (exception.Content.Value is ExceptionMessage)
                {
                    ExceptionMessage exMessage = exception.Content.Value as ExceptionMessage;

                    var exceptionMsg =
                        $"An AppException is initiated with HttpStatusCode: {(int)exception.HttpStatusCode}, \nOrginalAppException: {JsonConvert.SerializeObject(exMessage, Formatting.Indented, new JsonSerializerSettings { MaxDepth = 5 })}, \n OrginalException: {JsonConvert.SerializeObject(ex, Formatting.Indented, new JsonSerializerSettings { MaxDepth = 5 })}";

                    if (ex is Exceptions.ExternalRequestException || ex is Exceptions.ConfigException)
                    {
                        logger.Exception(new System.Exception(exceptionMsg));
                    }
                    else
                    {
                        logger.Warning(() => exceptionMsg);
                    }



                    if (exMessage.ErrorCodeValue != BaseErrorCodes.CommandValidation)
                    {
                        exMessage.IsDetailExposable = showNestedMessage;
                    }
                }

            }
            else
            {
                exception =
                    (AppException)
                    context.GetDependencyResolver()
                        .Resolve<InternalServerErrorException>()
                        .GetException(BaseErrorCodes.UnhandledException, ex, showNestedMessage, ex.Message);
                exceptionMessage = $"An exception occured with StatusCode {exception.HttpStatusCode}";
                logger
                .Exception(new System.Exception($"{exceptionMessage}, \n OrginalException:- {JsonConvert.SerializeObject(ex, Formatting.Indented, new JsonSerializerSettings { MaxDepth = 5 })}"));

            }



            context.Response.ReasonPhrase = exception.ReasonPhrase;
            context.Response.StatusCode = (int)exception.HttpStatusCode;
            context.Response.ContentType = "application/json";
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var json = JsonConvert.SerializeObject(exception.Content.Value, jsonSerializerSettings);

            context.Response.Write(json);
        }
    }

    /// <summary>
    ///     The owin exception handler middleware app builder extensions.
    /// </summary>
    public static class OwinExceptionHandlerMiddlewareAppBuilderExtensions
    {
        /// <summary>
        /// The use owin exception handler.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public static void UseOwinExceptionHandler(this IAppBuilder app)
        {
            app.Use((object)typeof(OwinExceptionHandlerMiddleware));
        }

        /// <summary>
        /// The use owin exception handler.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void UseOwinExceptionHandler<T>(this IAppBuilder app) where T : IExceptionConfiguration, new ()
        {
            app.Use((object)typeof(OwinExceptionHandlerMiddleware), new T());
        }
    }
}