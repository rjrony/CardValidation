using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Infrastructure.Host.HtmlEncoder
{
    /// <summary>
    /// The Action filter for santizing for Xss vulnerabilities
    /// </summary>
    public sealed class AntiXssFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionArgs = actionContext.ActionArguments;
            if (actionArgs != null)
            {
                foreach (var argumentValue in actionArgs.Values)
                {
                    if (argumentValue != null)
                    {
                        AntiXssEncoderHelper.EncodeProperties(argumentValue);
                    }
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
