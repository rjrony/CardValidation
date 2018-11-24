using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interception;

namespace Infrastructure.Web
{
    public class IntegrateCorrelationIdHandler : DelegatingHandler
    {
        public IntegrateCorrelationIdHandler()
        {
            InnerHandler = new HttpClientHandler();
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var operationContext = ServiceLocator.Instance.Current.ResolveSafeWithoutWarning<IOperationContext>();
            if (operationContext != null)
            {
                request.Headers.Add("CorrelationId", operationContext.CorrelationId);
            }

            return base.SendAsync(request, cancellationToken);

        }
    }
}
