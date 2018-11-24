// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionInvoker.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    ///     The TransactionInvoker interface.
    /// </summary>
    public interface IRetryingExecutionStrategyInvoker
    {
        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="httpMethod">
        /// The http Method.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResult> InvokeAsync<TResult>(string httpMethod, Func<Task<TResult>> operation,
            CancellationToken cancellationToken) where TResult : HttpResponseMessage;
    }
}