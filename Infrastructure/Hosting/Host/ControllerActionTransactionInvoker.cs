// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerActionTransactionInvoker.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;
    using System.Web.Http.Controllers;

    using Infrastructure.Host.Contracts;
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    /// <summary>
    ///     The controller action transaction invoker.
    /// </summary>
    public class ControllerActionTransactionInvoker : ApiControllerActionInvoker
    {
        private readonly IDependencyResolver dependencyResolver;

        
        public ControllerActionTransactionInvoker(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// The invoke action async.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task<HttpResponseMessage> InvokeActionAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var logger = this.dependencyResolver.Resolve<ILogger>();
            var transactionScopeProvider = this.dependencyResolver.Resolve<ITransactionScopeProvider>();


            var k = actionContext.Request.Method.Method.ToUpper();
            // TODO: This filtering will be off now
            if (actionContext.Request.Method.Method.ToUpper().Equals("GET"))
            {
                return await base.InvokeActionAsync(actionContext, cancellationToken);
            }

            using (var scope = transactionScopeProvider.Create())
            {
                logger.Debug(() => $"TransactionScope created with defaultTimeOut {TransactionManager.DefaultTimeout} and maxTimeOut {TransactionManager.MaximumTimeout}");

                HttpResponseMessage result = await base.InvokeActionAsync(actionContext, cancellationToken);
                
                if (result.IsSuccessStatusCode || result.StatusCode==HttpStatusCode.Redirect)
                {
                    scope.Complete();
                }
                else
                {
                    Transaction.Current.Rollback();
                    scope.Dispose();
                }

                return result;
            }
        }
    }
}