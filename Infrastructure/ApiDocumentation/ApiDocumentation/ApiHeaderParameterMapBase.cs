// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiHeaderParameterMapBase.cs" company="SS">
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
    using System.Reflection;

    using Infrastructure.ApiDocumentation.Contracts;
    using Infrastructure.Logging.Contracts;

    /// <summary>
    ///     The api header parameter map base.
    /// </summary>
    public abstract class ApiHeaderParameterMapBase : IApiHeaderParameterMap
    {

        private static readonly object lockedObject = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHeaderParameterMapBase" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected ApiHeaderParameterMapBase(ILogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// The parameters
        /// </summary>
        private static List<IEndPointParameters> parameterses;

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     The get api end point type.
        /// </summary>
        /// <returns>
        ///     The <see cref="Type" />.
        /// </returns>
        public abstract Type GetApiEndPointType();

        /// <summary>
        /// The get custom endpoint parameters.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public abstract List<EndPoint> GetCustomEndpoints();

        /// <summary>
        /// Builds the endpoints.
        /// </summary>
        /// <returns>
        /// List of EndPointParameters
        /// </returns>
        public List<IEndPointParameters> BuildEndpoints()
        {
            this.Logger.Debug(() => "Fetching endpoints");

            if (parameterses == null)
            {
                this.Logger.Debug(() => "Parameterses is null. Locking parameterses");

                lock (lockedObject)
                {
                    this.Logger.Debug(() => "Inside of lock");
                    if (parameterses == null)
                    {
                        this.Logger.Debug(() => "Endpoints are null, preparing endpoints");
                        List<EndPoint> endPoints = this.GetCustomEndpoints();
                        List<string> endpointNames = this.GetAllEndpointNames();
                        List<Type> types = this.GetTypesOfAllParameters();
                        List<IParameter> defaultParameters = types
                            .Select(
                                type => type.GetMethod("GetInstance", BindingFlags.Public | BindingFlags.Static)
                                            .Invoke(null, null) as IParameter).Where(type => type.IsDefault).ToList();
                        foreach (var endpointValue in endpointNames)
                        {
                            EndPoint endPoint = endPoints.FirstOrDefault(x => x.ApiEndPoint == endpointValue);
                            if (endPoint == null)
                            {
                                this.Logger.Debug(
                                    () => endpointValue + " doesn't exist. Creating new endpoint object and adding to list.");
                                endPoint = new EndPoint()
                                {
                                    ApiEndPoint = endpointValue,
                                    EndPointParameters = new List<IEndPointParamMap>(),
                                };

                                endPoints.Add(endPoint);
                            }

                            foreach (var parameter in defaultParameters)
                            {
                                EndPointParameter defaultParameterInMappedEndpoint =
                                    endPoint.EndPointParameters.FirstOrDefault(
                                        x => x.Parameter.GetType() == parameter.GetType()) as EndPointParameter;
                                if (defaultParameterInMappedEndpoint != null)
                                {
                                    if (!defaultParameterInMappedEndpoint.IsActive)
                                    {
                                        endPoint.EndPointParameters.Remove(defaultParameterInMappedEndpoint);

                                        this.Logger.Debug(
                                            () => "Removing Custom IsActive parameter for " + endPoint.ApiEndPoint);
                                    }

                                    this.Logger.Debug(
                                        () => parameter.Name + " or type -" + parameter.GetType().Name + " exists in list.");
                                }
                                else
                                {
                                    this.Logger.Debug(
                                        () => parameter.Name + " doesn't exist and default type, so adding to the list");
                                    IEndPointParamMap item = new EndPointParameter()
                                    {
                                        Parameter = parameter,
                                        IsActive = true
                                    };
                                    this.Logger.Debug(
                                        () => "Adding " + endPoint.ApiEndPoint + " of parameter " + parameter.Name
                                              + " in list");
                                    endPoint.EndPointParameters.Add(item);
                                }
                            }
                        }

                        parameterses = new List<IEndPointParameters>(endPoints);
                    }
                }
            }

            return parameterses;
        }

        private bool CheckInActive(IEndPointParamMap item)
        {
            bool inActive = (item as EndPointParameter)?.IsActive == false;
            if (!inActive)
            {
                this.Logger.Debug(() => "Removing " + item.Parameter.Name);
            }

            return inActive;
        }

        /// <summary>
        /// Gets all parameters.
        /// </summary>
        /// <returns>List of Types</returns>
        private List<Type> GetTypesOfAllParameters()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains(this.GetType().FullName.Split('.')[0])).SelectMany(s => s.GetTypes())
                .Where(p => typeof(IParameter).IsAssignableFrom(p) && !p.IsAbstract).ToList();
        }

        /// <summary>
        /// Gets all endpoints.
        /// </summary>
        /// <returns>All endpoint names</returns>
        private List<string> GetAllEndpointNames()
        {
            this.Logger.Debug(() => "Fetching - type of ApiPaths");
            Type endPointType = this.GetApiEndPointType();
            this.Logger.Debug(() => "Fetching all fields. eg. public const string LookupCountry = 'Lookup / Country';");
            FieldInfo[] allEndpoints = endPointType.GetFields(BindingFlags.Static | BindingFlags.Public);
            List<string> endpointNames = allEndpoints.Select(x => x.GetValue(null).ToString()).ToList();
            return endpointNames;
        }
    }
}