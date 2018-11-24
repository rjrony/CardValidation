// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoContentResult.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.Contracts
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// The no content result.
    /// </summary>
    public class NoContentResult : OkResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentResult"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        public NoContentResult(ApiController controller)
            : base(controller)
        {
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
        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = base.ExecuteAsync(cancellationToken).Result;
            response.StatusCode = HttpStatusCode.NoContent;
            return Task.FromResult(response);
        }
    }
}