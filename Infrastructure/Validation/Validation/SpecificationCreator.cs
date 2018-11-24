// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecificationCreator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    

    using Infrastructure.Interception.Contract;

    using SpecExpress;

    /// <summary>
    ///     The specification creator.
    /// </summary> 
    public class SpecificationCreator
    {
        private readonly List<SpecificationBase> registry = new List<SpecificationBase>();

        private readonly IDependencyResolver resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationCreator"/> class.
        /// </summary>
        /// <param name="resolver">
        /// The resolver.
        /// </param>
        public SpecificationCreator(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        /// <summary>
        /// The create specifications.
        /// </summary>
        /// <param name="specs">
        /// The specs.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<SpecificationBase> CreateSpecifications(IEnumerable<Type> specs)
        {
            int counter = 0;
            int max = 10;

            var delayedSpecs = this.CreateAndRegisterSpecificationsWithRegistryIterator(specs, counter, max);

            while (delayedSpecs.Any())
            {
                counter++;
                delayedSpecs = this.CreateAndRegisterSpecificationsWithRegistryIterator(specs, counter, max);
            }

            return this.registry;
        }

        private void Add(SpecificationBase spec)
        {
            if (spec != null)
            {
                this.registry.Add(spec);
            }
        }

        private List<Type> CreateAndRegisterSpecificationsWithRegistryIterator(IEnumerable<Type> specs, int counter, int max)
        {
            //TODO: This can result in a stackoverflow if a ForEachSpecification<Type> never finds a default spec for Type
            var delayedSpecs = new List<Type>();

            //For each type, instantiate it and add it to the collection of specs found
            specs.ToList<Type>().ForEach(
                spec =>
                    {
                        // Prevent two of the same specification from being registered
                        if (
                            !(from specification in this.registry
                              where specification.GetType().FullName == spec.FullName
                              select specification).Any())
                        {
                            try
                            {
                                var s = this.resolver.Resolve(spec) as SpecificationBase;

                                this.Add(s);
                            }
                            catch (TargetInvocationException te)
                            {
                                if (counter > max)
                                {
                                    throw new SpecExpressConfigurationException(
                                        string.Format("Exception thrown while trying to register {0}.", spec.FullName),
                                        te);
                                }
                                else
                                {
                                    //Can't create the object because it has a specification that hasn't been loaded yet
                                    //save it for the next pass
                                    delayedSpecs.Add(spec);
                                }
                            }
                            catch (Exception err)
                            {
                                throw new SpecExpressConfigurationException(
                                    string.Format("Exception thrown while trying to register {0}.", spec.FullName),
                                    err);
                            }
                        }
                    });

            return delayedSpecs;

            //Process any specification that couldn't be reloaded
            //if (delayedSpecs.Any())
            //{
            //    Add(delayedSpecs);
            //}
        }
    }
}