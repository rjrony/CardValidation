using System.Threading.Tasks;
using Host.Owin;
using Infrastructure.Interception.Contract;
using Infrastructure.SessionManagement.Contracts;
using Microsoft.Owin;
using Owin;
using System.Linq;

namespace Infrastructure.SessionManagement
{
    /// <summary>
    /// The request middleware for request specific operation
    /// </summary>
    public class RequestMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RequestMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            var dependencyResolver = context.GetDependencyResolver().Resolve<IDependencyResolver>();
            var requestInfo = dependencyResolver.Resolve<IRequestInfo>();
            var ipAddress = context.Request.Headers.ContainsKey("x-forwarded-for") ? (string.IsNullOrEmpty(context.Request.Headers.GetValues("x-forwarded-for").First()) ? context.Request.RemoteIpAddress : context.Request.Headers.GetValues("x-forwarded-for").First().Split(',').First().Split(':').First()) : context.Request.RemoteIpAddress;
            requestInfo.IpAddress = ipAddress;
            await this.Next.Invoke(context);
        }
    }

    public static class RequestMiddlewareAppBuilderExtensions
    {
        public static void UseRequestMiddleware(this IAppBuilder app)
        {
            app.Use(typeof(RequestMiddleware));
        }
    }
}
