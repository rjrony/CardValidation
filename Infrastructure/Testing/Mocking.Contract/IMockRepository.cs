// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMockRepository.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Mocking.Contract
{
    using System.Collections.Generic;

    /// <summary>
    /// The MockRepository interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IMockRepository<T>
        where T : class, new()
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Get();

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> List(int count);
    }
}