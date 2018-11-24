// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Security.AntiXss;
using Infrastructure.Host.HtmlEncoder;

namespace Infrastructure.Host
{
    using System.Runtime.Serialization.Formatters;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Infrastructure.Exception;
    using Infrastructure.Host.Contracts;
    using Infrastructure.Host.ExceptionHandling;
    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Microsoft.Practices.Unity;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using ITraceWriter = System.Web.Http.Tracing.ITraceWriter;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        private static ILogger cachedLogger;

        /// <summary>
        /// The disable web api default exception handler.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig DisableWebApiDefaultExceptionHandler(this IBootstrapperConfig config)
        {
            config.Configuration.Services.Replace(typeof(System.Web.Http.ExceptionHandling.IExceptionHandler), new PassthroughException());
            return config;
        }

        /// <summary>
        /// The enable global exception handler.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig EnableGlobalExceptionHandler(this IBootstrapperConfig config)
        {
            var handler = config.Container.ResolveSafe<IExceptionHandler>(config.Logger()) ?? new ExceptionHandler();
            var globalExceptionHandler = new GlobalExceptionHandler(handler, config.Logger());
            ////GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.ExceptionHandling.IExceptionHandler), globalExceptionHandler);
            config.Configuration.Services.Replace(typeof(System.Web.Http.ExceptionHandling.IExceptionHandler), globalExceptionHandler);
            config.Container.RegisterInstance<System.Web.Http.ExceptionHandling.IExceptionHandler>(globalExceptionHandler);
            return config;
        }

        /// <summary>
        /// handle the $type field of the json data
        /// </summary>
        /// <param name="config">
        /// The bootstrapper config
        /// </param>
        /// <param name="typeNameHandling">
        /// TypeNameHandling
        /// </param>
        /// <param name="formatterAssemblyStyle">
        /// FormatterAssemblyStyle
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig EnableJsonTypeNameHandling(
            this IBootstrapperConfig config,
            TypeNameHandling typeNameHandling = TypeNameHandling.All,
            FormatterAssemblyStyle formatterAssemblyStyle = FormatterAssemblyStyle.Simple)
        {
            /*
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = typeNameHandling;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameAssemblyFormat = formatterAssemblyStyle;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Error = HandleJsonSerializationErrors;*/
            config.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = typeNameHandling;
            config.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameAssemblyFormat = formatterAssemblyStyle;

            config.Configuration.Formatters.JsonFormatter.SerializerSettings.Error = HandleJsonSerializationErrors;

            return config;
        }

        /// <summary>
        /// The enable logging.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="isEnabled">
        /// The is enabled.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig EnableLogging(this IBootstrapperConfig config, bool isEnabled = true)
        {
            ((BootstrapperConfig)config).IsLoggingEnabled = isEnabled;

            var logger = config.Container.ResolveSafe<ILogger>(new NullLogger());

            // add the default logger to container
            if (logger == null)
            {
                logger = new Infrastructure.Logging.Logger(config.Container.Resolve<IDependencyResolver>());
                logger.Debug(() => "Register ILogger on the container");
                config.Container.RegisterInstance(logger);
            }

            // replace the default tracewriter
            logger.Debug(() => "Check if logger is ITraceWriter");
            if (logger is ITraceWriter)
            {
                logger.Debug(() => "Logger is ITraceWriter so replace the service on Configuration");
                ////GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), logger);
                config.Configuration.Services.Replace(typeof(ITraceWriter), logger);
            }

            return config;
        }

        /// <summary>
        /// The enable transaction.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="isEnabled">
        /// The is enabled.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig EnableTransaction(this IBootstrapperConfig config, bool isEnabled = true)
        {
            var logger = config.Container.ResolveSafe<ILogger>(new NullLogger());

            ((BootstrapperConfig)config).IsTransactionEnabled = isEnabled;

            if (((BootstrapperConfig)config).IsTransactionEnabled)
            {
                var controllerActionTransactionInvoker = config.Container.Resolve<ControllerActionTransactionInvoker>();
                logger.Debug(() => "ControllerActionTransactionInvoker initiated");
                //config.Configuration.Services.Replace(typeof(IHttpActionInvoker), new ControllerActionTransactionInvoker());
                config.Configuration.Services.Replace(typeof(IHttpActionInvoker), controllerActionTransactionInvoker);
            }

            return config;
        }

        /// <summary>
        /// The enable user message.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="resourceSet">
        /// The resource set.
        /// </param>
        /// <returns>
        /// The <see cref="IBootstrapperConfig"/>.
        /// </returns>
        public static IBootstrapperConfig EnableUserMessage(this IBootstrapperConfig config, string resourceSet)
        {
            ResourceProvider.ResourceSet = resourceSet;
            return config;
        }


        public static IBootstrapperConfig EnableAntiXssEncoding(this IBootstrapperConfig config)
        {
            config.Configuration.Filters.Add(new AntiXssFilter());
            return config;
        }

        /// <summary>
        /// The is null.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <typeparam name="T">
        /// Type
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>ss
        /// </returns>
        public static bool IsNull<T>(this T subject)
        {
            return ReferenceEquals(subject, null);
        }

        /// <summary>
        /// The logger.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <returns>
        /// The <see cref="ILogger"/>.
        /// </returns>
        public static ILogger Logger(this object instance)
        {
            if (cachedLogger != null)
            {
                return cachedLogger;
            }

            object traceWriter = GlobalConfiguration.Configuration.Services.GetService(typeof(ITraceWriter));
            cachedLogger = traceWriter as ILogger;
            return cachedLogger ?? new NullLogger();
        }

        /// <summary>
        /// The handle json serialization errors.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="errorEventArgs">
        /// The errors event args.
        /// </param>
        private static void HandleJsonSerializationErrors(object sender, ErrorEventArgs errorEventArgs)
        {
            string errorMessage = string.Format(
                "Path: {0} - Member: {1} - Message: {2} StackTrace: {3}",
                errorEventArgs.ErrorContext.Path,
                errorEventArgs.ErrorContext.Member,
                errorEventArgs.ErrorContext.Error.Message,
                errorEventArgs.ErrorContext.Error.StackTrace);
            //GlobalConfiguration.Configuration.DependencyResolver.Logger().Error(() => errorMessage);//ToDo have to do something
        }

        /// <summary>
        /// Deserilize Json with Anti Xss Encoding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The Json string that needs deserilization</param>
        /// <param name="errors">The error field-Message collection</param>
        /// <returns></returns>
        public static T DeserializeJsonWithAntiXssEncoding<T>(this string jsonString, out ICollection<KeyValuePair<string, string>> errors)
        {
            errors = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new AntiXssContractResolver()
            };
            var errorCopy = errors;
            jsonSerializerSettings.Error = (o, args) => HandleDeserializationError(o, args, errorCopy);

            return JsonConvert.DeserializeObject<T>(jsonString,
                jsonSerializerSettings);
        }

        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs, ICollection<KeyValuePair<string, string>> errors)
        {
            var errorArgsErrorContext = errorArgs.ErrorContext;
            errors.Add(new KeyValuePair<string, string>(errorArgsErrorContext.Path, AntiXssEncoder.HtmlEncode(errorArgsErrorContext.Error.Message,true)));
            errorArgsErrorContext.Handled = true;
        }
    }
}