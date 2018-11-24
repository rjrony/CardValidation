// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomOperationFilter.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.ApiDocumentation.Contracts
{
    using System.Web.Http.Description;

    using Swashbuckle.Swagger;

    /// <summary>
    ///     The CustomOperationFilter interface.
    /// </summary>
    public interface ICustomOperationFilter
    {
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
        void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription);
    }
}