// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerConfig.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Http;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.ApiDocumentation.Contracts;

    using Swashbuckle.Application;

    /// <summary>
    ///     The swagger config.
    /// </summary>
    public class SwaggerConfig
    {
        private static IApiDocConfig docConfig;

        /// <summary>
        ///     Gets or sets the application path.
        /// </summary>
        public static string ApplicationPath { get; set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        [Microsoft.Practices.Unity.Dependency]
        public static ILogger Logger { get; set; } // TODO: Temporary solution, need to check GC dispose for static property

        /// <summary>
        /// The get all interfaces.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<Type> GetAllInterfaces()
        {
            IList<Type> types = new List<Type>();
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            var dlls = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().FullName.Contains(assemblyName));

            foreach (var dll in dlls)
            {
                var listOfInterfaceArrays = from t in dll.GetTypes() select t.GetInterfaces();

                foreach (var arrayOfInterfaces in listOfInterfaceArrays)
                {
                    foreach (var theInterface in arrayOfInterfaces)
                    {
                        types.Add(theInterface);
                    }
                }
            }

            return types;
        }

        /// <summary>
        /// The get implementations.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<Type> GetImplementations<T>()
        {
            var dlls = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().FullName.Contains(docConfig.AssemblyPrefixDetailed));
            //var implementations = dlls.SelectMany(dll => (from t in dll.GetTypes() where t.IsInstanceOfType(typeof(T)) select t)).ToList();
            var implementations = dlls.SelectMany(dll => (from t in dll.GetTypes() where typeof(T).IsAssignableFrom(t) select t)).ToList();

            return implementations;
        }

        /// <summary>
        /// The has implementation.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasImplementation<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Any(p => p.GetTypes().Any(t => t.IsAssignableFrom(typeof(T))));
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config, Infrastructure.Interception.Contract.IDependencyResolver dependencyResolver, IApiDocConfig docConfig)
        {
            //   IDependencyResolver dependencyResolver = (IDependencyResolver)webDependencyResolver.GetService(typeof(IDependencyResolver));
            //docConfig = dependencyResolver.Resolve<IApiDocConfig>();
            

            ApplicationPath = $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\{docConfig.XmlCommentFilePathBase}";
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            var apiDocConstantValueProvider = new ApiDocConstant();

            Logger = new NullLogger();

            Logger.Debug(() => "Starting registration of swagger configurations");
            config.EnableSwagger(
                "docs/{apiVersion}/swagger",
                c =>
                    {
                        c.SingleApiVersion(docConfig.Version1Name, docConfig.AppName).Description(docConfig.DocumentHeader);
                        c.SchemaId(x => x.FullName);
                        c.RootUrl(req => $"{req.RequestUri.Scheme}://{req.RequestUri.Authority}{docConfig.AppVirtualPath}");

                        var xmlCommentsPathForApiControllers = GetXmlCommentsPath(docConfig.XmlCommentFileApi);
                        var xmlCommentsPathForDtos = GetXmlCommentsPath(docConfig.XmlCommentFileDto);

                        if (!string.IsNullOrEmpty(xmlCommentsPathForApiControllers))
                        {
                            c.IncludeXmlComments(xmlCommentsPathForApiControllers);
                        }

                        if (!string.IsNullOrEmpty(xmlCommentsPathForDtos))
                        {
                            c.IncludeXmlComments(xmlCommentsPathForDtos);
                        }

                        //c.DocumentFilter(() => new CustomDocumentFilter(dependencyResolver));
                        //c.SchemaFilter(() => new ExampleSchemaFilter(dependencyResolver));
                        //c.OperationFilter(() => new HeaderParameterOperationFilter(dependencyResolver));
                    }).EnableSwaggerUi(
                        "docs/{*assetPath}",
                        c =>
                            {
                                c.InjectStylesheet(thisAssembly, apiDocConstantValueProvider.CssPath);
                                c.InjectJavaScript(thisAssembly, apiDocConstantValueProvider.JsPath);
                                c.DisableValidator();
                            });
        }

        /// <summary>
        /// The get xml comments path.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetXmlCommentsPath(string fileName)
        {
            var path = $@"{ApplicationPath}\{fileName}";

            if (File.Exists(path))
            {
                return path;
            }

            return null;
        }
    }
}