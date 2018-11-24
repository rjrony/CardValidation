// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomDocumentFilter.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Description;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.ApiDocumentation.Contracts;

    using Swashbuckle.Swagger;

    /// <summary>
    /// The custom document filter.
    /// </summary>
    public class CustomDocumentFilter : IDocumentFilter
    {
        private readonly IDependencyResolver dependencyResolver;

        private IApiCustomEndPoint apiCustomEndPoint;

        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDocumentFilter"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency resolver.
        /// </param>
        public CustomDocumentFilter(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// The apply.
        /// </summary>
        /// <param name="swaggerDoc">
        /// The swagger doc.
        /// </param>
        /// <param name="schemaRegistry">
        /// The schema registry.
        /// </param>
        /// <param name="apiExplorer">
        /// The api explorer.
        /// </param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            if (!SwaggerConfig.GetImplementations<IApiCustomEndPoint>().Any())
            {
                return;
            }

            this.logger = this.dependencyResolver.Resolve<ILogger>();
            this.apiCustomEndPoint = this.dependencyResolver.Resolve<IApiCustomEndPoint>();

            foreach (var pair in this.apiCustomEndPoint.CustomEndPoints())
            {
                this.logger?.Debug(() => $"Custom parameter binding started for: {pair.EndPoint} end point");

                swaggerDoc.paths.Add(
                    string.Concat("/", pair.EndPoint),
                    new PathItem
                        {
                            post = new Operation
                                       {
                                           tags = pair.GroupList,
                                           //description = "The authentication token",
                                           consumes = new List<string> { "application/x-www-form-urlencoded" },
                                           parameters = this.BuildParameter(pair.Parameters)
                                       }
                        });
            }
        }

        private List<Parameter> BuildParameter(List<IParameter> mappedParameters)
        {
            return (from endPointParamMap in mappedParameters
                    where !endPointParamMap.GetType().IsAbstract
                    select
                        (IParameter)
                        Activator.CreateInstance(
                            endPointParamMap.GetType(),
                            endPointParamMap.Name,
                            endPointParamMap.Description,
                            endPointParamMap.In,
                            endPointParamMap.Type,
                            endPointParamMap.Required,
                            endPointParamMap.IsDefault)
                    into obj
                    select
                        new Parameter
                            {
                                name = obj.Name,
                                @in = obj.In,
                                type = obj.Type,
                                required = obj.Required,
                                description = obj.Description
                            }).ToList();
        }
    }
}