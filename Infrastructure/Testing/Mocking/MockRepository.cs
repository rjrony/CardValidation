// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockRepository.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Mocking
{
    using System.Collections.Generic;

    using Infrastructure.Mocking.Contract;

    /// <summary>
    /// The mock repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class MockRepository<T> : IMockRepository<T>
        where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockRepository{T}"/> class.
        /// </summary>
        protected MockRepository()
        {
            this.Content = new T();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T Get();

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public abstract IEnumerable<T> List(int count);
    }
}