// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiDocumentMockRepository.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Mocking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure;

    using Infrastructure.Mocking.Contract;

    /// <summary>
    ///     The api document mock repository.
    /// </summary>
    public static class ApiDocumentMockRepository
    {
        /// <summary>
        ///     The get.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T Get<T>() where T : class, new()
        {
            var type = CurrentDomainTypes.GetTypesDerivingFrom<MockRepository<T>>(isIncludingAbstract: false).FirstOrDefault();

            if (type == null)
            {
                return new T();
            }

            var instance = Activator.CreateInstance(type);
            var example = ((IMockRepository<T>)instance).Get();

            return example;
        }

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> List<T>(int count) where T : class, new()
        {
            var type = CurrentDomainTypes.GetTypesDerivingFrom<MockRepository<T>>(isIncludingAbstract: false).FirstOrDefault();

            if (type == null)
            {
                return (IEnumerable<T>)new T();
            }

            var instance = Activator.CreateInstance(type);
            var example = ((IMockRepository<T>)instance).List(count);

            return example;
        }
    }
}