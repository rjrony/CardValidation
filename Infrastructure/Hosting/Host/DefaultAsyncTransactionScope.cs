// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultAsyncTransactionScope.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   The default async transaction scope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System.Transactions;

    using Infrastructure.Host.Contracts;

    /// <summary>
    /// The default async transaction scope.
    /// </summary>
    public class DefaultAsyncTransactionScope : ITransactionScopeProvider
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="TransactionScope"/>.
        /// </returns>
        public TransactionScope Create()
        {
            var transactionOptions = new TransactionOptions
                                         {
                                             IsolationLevel = IsolationLevel.ReadCommitted
                                         };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}