// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExceptionHandler.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.Contracts
{
    using System.Threading;
    using System.Web.Http.ExceptionHandling;

    /// <summary>
    ///     The ExceptionHandler interface.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation Token.
        /// </param>
        void HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken);
    }
}