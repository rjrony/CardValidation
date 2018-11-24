// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderParameterOperationFilter.cs" company="BB">
//   Copyright © 2017. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
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

    using Microsoft.Practices.ObjectBuilder2;

    using Swashbuckle.Swagger;

    /// <summary>
    ///     The header parameter operation filter.
    /// </summary>
    public class HeaderParameterOperationFilter : IOperationFilter
    {
        private readonly IDependencyResolver dependencyResolver;

        private IApiHeaderParameterMap apiHeaderParameterMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderParameterOperationFilter"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency Resolver.
        /// </param>
        public HeaderParameterOperationFilter(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// The apply.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="schemaRegistry">
        /// The schema registry.
        /// </param>
        /// <param name="apiDescription">
        /// The api description.
        /// </param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            IList<Parameter> preBindedParameters = new List<Parameter>();

            ILogger logger = this.dependencyResolver.Resolve<ILogger>();
            logger.Debug(() => "Starting parameter setup for documentation");

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            else
            {
                foreach (var parameter in operation.parameters)
                {
                    preBindedParameters.Add(parameter);
                }

                operation.parameters.Clear();
                operation.parameters = new List<Parameter>();
            }

            var clearEndPointPath = apiDescription.RelativePath.Contains(ApiDocConstant.QueryStringSeparatorForUrl)
                                        ? apiDescription.RelativePath.Split(ApiDocConstant.QueryStringSeparatorForUrl)[0]
                                        : apiDescription.RelativePath;
            this.apiHeaderParameterMap = this.dependencyResolver.Resolve<IApiHeaderParameterMap>();
            List<IEndPointParameters> paramList = this.apiHeaderParameterMap.BuildEndpoints();

            var endPointWiseParamGroup =
                paramList.Where(x => x.ApiEndPoint == clearEndPointPath)
                    .GroupBy(item => item.ApiEndPoint, (key, group) => new { EndPoint = key, Items = group.ToList() })
                    .ToList();

            foreach (var group in endPointWiseParamGroup)
            {
                if (group.EndPoint == clearEndPointPath)
                {
                    List<IEndPointParamMap> headerParams = new List<IEndPointParamMap>();
                    List<IEndPointParamMap> formDataParams = new List<IEndPointParamMap>();
                    List<IEndPointParamMap> otherParams = new List<IEndPointParamMap>();

                    foreach (IEndPointParameters item in group.Items)
                    {
                        List<IEndPointParamMap> pointParamMaps = item.EndPointParameters.FindAll(
                            x => x.Parameter.GetType().IsSubclassOf(typeof(HeaderParameterBase)));
                        headerParams.AddRange(pointParamMaps);

                        List<IEndPointParamMap> endPointParamMaps = item.EndPointParameters.FindAll(
                            x => x.Parameter.GetType().IsSubclassOf(typeof(FormDataParameterBase)));
                        formDataParams.AddRange(endPointParamMaps);

                        List<IEndPointParamMap> paramMaps = item.EndPointParameters.FindAll(
                            x => !(x.Parameter.GetType().IsSubclassOf(typeof(HeaderParameterBase)) || x.Parameter
                                       .GetType().IsSubclassOf(typeof(FormDataParameterBase))));
                        otherParams.AddRange(paramMaps);
                    }
                    
                    this.BindParameter(operation, headerParams);
                    this.BindParameter(operation, formDataParams);
                    this.BindParameter(operation, otherParams);

                    if (preBindedParameters.Count > 0)
                    {
                        preBindedParameters.ForEach(parameter => operation.parameters.Add(parameter));
                    }
                }
            }
        }

        /// <summary>
        /// Binds the parameter.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="parameters">The parameters.</param>
        private void BindParameter(Operation operation, List<IEndPointParamMap> parameters)
        {
            foreach (var endPointParamMap in parameters)
            {
                if (!endPointParamMap.GetType().IsAbstract)
                {
                    operation.parameters.Add(
                        new Parameter
                            {
                                name = endPointParamMap.Parameter.Name,
                                @in = endPointParamMap.Parameter.In,
                                type = endPointParamMap.Parameter.Type,
                                required = endPointParamMap.Parameter.Required,
                                description = endPointParamMap.Parameter.Description, 
                            });
                }
            }
        }
    }
}