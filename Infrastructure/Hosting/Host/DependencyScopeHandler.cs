// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyScopeHandler.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Host
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Hosting;

    using global::Host.Owin;

    /// <summary>
    ///     The dependency scope handler.
    /// </summary>
    public class DependencyScopeHandler : DelegatingHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyScopeHandler" /> class.
        /// </summary>
        public DependencyScopeHandler()
        {
        }

        //[SecuritySafeCritical]
        //[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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
        /// <exception cref="ArgumentNullException">
        /// </exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var owinContext = request.GetOwinContext();
            if (owinContext == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            var dependencyResolver = owinContext.GetScopedApplicationDependecyResolver();
            if (dependencyResolver == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            //  var dependencyScope = new AutofacWebApiDependencyScope(lifetimeScope);
            request.Properties[HttpPropertyKeys.DependencyScope] = new WebApiDependencyScope(dependencyResolver);

            return base.SendAsync(request, cancellationToken);
        }
    }
}