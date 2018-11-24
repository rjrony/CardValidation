// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionScopeProvider.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.Contracts
{
    using System.Transactions;

    /// <summary>
    /// The TransactionScopeProvider interface.
    /// </summary>
    public interface ITransactionScopeProvider
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="TransactionScope"/>.
        /// </returns>
        TransactionScope Create();
    }
}