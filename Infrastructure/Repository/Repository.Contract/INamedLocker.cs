// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INamedLocker.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Repository.Contracts
{
    using System;

    /// <summary>
    ///     The NamedLocker interface.
    /// </summary>
    public interface INamedLocker : IDisposable
    {
        /// <summary>
        ///     The commit.
        /// </summary>
        void Commit();

        /// <summary>
        /// The lock.
        /// </summary>
        /// <param name="lockName">
        /// The lock name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Lock(string lockName);

        /// <summary>
        ///     The rollback.
        /// </summary>
        void Rollback();
    }
}