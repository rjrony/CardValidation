// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseSpecificationTests.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The base specification tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.DependencyContainerBuilder;
using Infrastructure.Interception;
using Infrastructure.Interception.Contract;
using Infrastructure.Validation.Contracts;
using SpecExpress;

namespace CardValidation.Api.Dtos.Validation.Tests
{
    /// <summary>
    ///     The base specification tests.
    /// </summary>
    public class BaseSpecificationTests
    {
        private static IMessageValidator MessageValidator;

        //public static IDependencyResolver DependencyResolver;

        /// <summary>
        ///     The validate.
        /// </summary>
        /// <param name="toValidationInstance">
        ///     The to validation instance.
        /// </param>
        /// <returns>
        ///     The <see cref="ValidationNotification" />.
        /// </returns>
        public ValidationNotification Validate(object toValidationInstance)
        {
            return MessageValidator.Validate(toValidationInstance);
        }

        /// <summary>
        ///     The class initialize.
        /// </summary>
        protected static void ClassInitialize()
        {
            //EffortProviderConfiguration.RegisterProvider();
            var containerBuilder = new DependencyContainerBuilder(new HostUnityContainer());
            var container = containerBuilder.BuildUnityContainer();
            var dependencyResolver = container.Resolve(typeof(IDependencyResolver), string.Empty) as IDependencyResolver;

            //DbInitializer dbInitializer = dependencyResolver.Resolve(typeof(DbInitializer)) as DbInitializer;

            //Database.SetInitializer(dbInitializer);

            MessageValidator = dependencyResolver.Resolve<IMessageValidator>();
        }
    }
}