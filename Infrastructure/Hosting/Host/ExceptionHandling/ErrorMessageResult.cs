// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorMessageResult.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.ExceptionHandling
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    /// <summary>
    ///     The errors message result.
    /// </summary>
    public class ErrorMessageResult : IHttpActionResult
    {
        private readonly HttpResponseMessage httpResponseMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessageResult"/> class.
        /// </summary>
        /// <param name="httpResponseMessage">
        /// The http response message.
        /// </param>
        public ErrorMessageResult(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.httpResponseMessage);
        }
    }
}