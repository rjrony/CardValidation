// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationContextUnitOfWork.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Infrastructure.Interception;
    using Infrastructure.Interception.Contract;

    using SpecExpress;

    /// <summary>
    ///     The validation context unit of work.
    /// </summary>
    public class ValidationContextUnitOfWork : ValidationContext
    {
        private static List<Type> foundSpecifications = null;

        [ThreadStatic]
        private static IDependencyResolver resolverInternal;

        [ThreadStatic]
        private static SpecificationContainer unitOfWorkSpecificationContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationContextUnitOfWork" /> class.
        /// </summary>
        public ValidationContextUnitOfWork()
        {
            this.FillContextContainer();
        }

        /// <summary>
        ///     The get found specifications count.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int GetFoundSpecificationsCount()
        {
            return foundSpecifications != null ? foundSpecifications.Count - 1 : 0;
        }

        /// <summary>
        /// The scan for specification.
        /// </summary>
        /// <param name="isForce">
        /// The is force.
        /// </param>
        public static void ScanForSpecification(bool isForce = false)
        {
            isForce = isForce || foundSpecifications == null;

            if (!isForce)
            {
                return;
            }

            var scanner = new SpecificationScanner();
            scanner.AddAssemblies(CurrentDomainTypes.GetAssemblies().ToList());

            var fieldSpecsFound = scanner.GetType().GetField("_specifications", BindingFlags.Instance | BindingFlags.NonPublic);
            foundSpecifications = (List<Type>)fieldSpecsFound.GetValue(scanner);

            if (foundSpecifications.Count == 0)
            {
                foundSpecifications.Add(typeof(DummySpecification));
            }
        }

        internal static void SetUnitOfWorkId(IDependencyResolver resolver)
        {
            if (resolver != resolverInternal)
            {
                unitOfWorkSpecificationContainer = null;
            }

            resolverInternal = resolver;
        }

        private void FillContextContainer()
        {
            if (resolverInternal == null)
            {
                resolverInternal =
                    ServiceLocator.Instance.GetRootContainer().Resolve(typeof(IDependencyResolver), null) as IDependencyResolver;
            }

            var creator = new SpecificationCreator(resolverInternal);
            if (unitOfWorkSpecificationContainer != null)
            {
                this.SpecificationContainer.Add(unitOfWorkSpecificationContainer.GetAllSpecifications());
                return;
            }

            ScanForSpecification(true);

            var instances = creator.CreateSpecifications(foundSpecifications);
            this.SpecificationContainer.Add(instances);
            unitOfWorkSpecificationContainer = this.SpecificationContainer;
        }
    }
}