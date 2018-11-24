// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PassthroughExceptionHandler.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Host
{
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;

    /// <summary>
    /// The passthrough exception.
    /// </summary>
    public class PassthroughException : IExceptionHandler
    {
        /// <summary>
        /// The handle async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // don't just throw the exception; that will ruin the stack trace
            var info = ExceptionDispatchInfo.Capture(context.Exception);
            info.Throw();
            return Task.FromResult<object>(null);
        }
    }
}