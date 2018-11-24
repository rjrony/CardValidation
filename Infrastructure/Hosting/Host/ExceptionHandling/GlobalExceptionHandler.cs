// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalExceptionHandler.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.ExceptionHandling
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;

    using global::Infrastructure.Logging.Contracts;

    using IExceptionHandler = Infrastructure.Host.Contracts.IExceptionHandler;

    /// <summary>
    ///     The global exception handler.
    /// </summary>
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        private readonly IExceptionHandler exceptionHandler;

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="exceptionHandler">
        /// The exception Handler.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Must not be empty
        /// </exception>
        public GlobalExceptionHandler(IExceptionHandler exceptionHandler, ILogger logger)
        {
            if (exceptionHandler == null)
            {
                throw new ArgumentNullException("exceptionHandler");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.exceptionHandler = exceptionHandler;
            this.logger = logger;
        }

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
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            this.exceptionHandler.HandleAsync(context, cancellationToken);
            return base.HandleAsync(context, cancellationToken);
        }

        /// <summary>
        /// The should handle.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}