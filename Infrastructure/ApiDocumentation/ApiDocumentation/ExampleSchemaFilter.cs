// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExampleSchemaFilter.cs" company="SS">
//   Copyright © 2017. All rights reserved.
// </copyright>
// <summary>
//   The example schema filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

 namespace Infrastructure.ApiDocumentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.Mocking;

    using Swashbuckle.Swagger;

    /// <summary>
    ///     The example schema filter.
    /// </summary>
    public class ExampleSchemaFilter : ISchemaFilter
    {
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleSchemaFilter"/> class.
        /// </summary>
        /// <param name="dependencyResolver">
        /// The dependency resolver.
        /// </param>
        public ExampleSchemaFilter(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// The apply.
        /// </summary>
        /// <param name="schema">
        /// The schema.
        /// </param>
        /// <param name="schemaRegistry">
        /// The schema registry.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            ILogger logger = this.dependencyResolver.Resolve<ILogger>();
            logger.Debug(() => "Starting example setup for documentation");

            Type tempType = typeof(ApiDocumentMockRepository);
            MethodInfo method = tempType.GetMethod("Get");
            
            bool typeIsInterface = type.IsInterface;

            if (!typeIsInterface)
            {
                MethodInfo genericMethod = method.MakeGenericMethod(type);
                var example = genericMethod.Invoke(null, null);
                if (example != null)
                {
                    var enumerableExample = example.GetType().GetProperties().Select(pi => pi.GetValue(example));

                    var ignorable = enumerableExample.Any(propVal => propVal == null);

                    if (ignorable)
                    {
                        return;
                    }

                    schema.example = example;
                }
            }           
        }        
    }
}