// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestHandler.cs" company="SS">
//   Copyright © SS. All rights reserved.
// </copyright>
// <summary>
//   The request handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Interception.Contract;
using Infrastructure.Logging.Contracts;
using Infrastructure.SessionManagement.Contracts;

namespace CardValidation.Api
{
    /// <summary>
    ///     The request handler.
    /// </summary>
    public class RequestHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="validator">The validator.</param>
        /// <exception cref="System.ArgumentNullException">logger</exception>
        /// <exception cref="ArgumentNullException">must not be null</exception>
        public RequestHandler(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.Logger = logger;
        }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //this.AddMessageToContenxt(request);
            var requestInfo = (IRequestInfo)request.GetDependencyScope().GetService(typeof(IRequestInfo));
            IDependencyResolver dependencyResolver =
                (IDependencyResolver)request.GetDependencyScope().GetService(typeof(IDependencyResolver));

            requestInfo.Headers = request.Headers.Select(h => h);
           
            

            requestInfo.HostName = request.RequestUri.AbsoluteUri.Substring(
                0,
                request.RequestUri.AbsoluteUri.Length - request.RequestUri.LocalPath.Length);

            if (request.Headers.Authorization != null)
            {
                requestInfo.AuthorizationParameter = request.Headers.Authorization.Parameter;
                requestInfo.AuthorizationScheme = request.Headers.Authorization.Scheme;
                //this.Logger.Debug(
                //    () => $"request.Headers.Authorization.AuthorizationParameter is {request.Headers.Authorization.Parameter}");
            }
            else
            {
                this.Logger.Warning(() => string.Format("request.Headers.Authorization is null at session handler"));
            }

            // common header validation checking 
            return base.SendAsync(request, cancellationToken);
        }


        private void AddMessageToContenxt(HttpRequestMessage request)
        {
            const string HttpRequestMessageKey = "MS_HttpRequestMessage";

            if (HttpContext.Current.Items.Contains(HttpRequestMessageKey))
            {
                return;
            }

            HttpContext.Current.Items.Add(HttpRequestMessageKey, request);
        }
    }
}