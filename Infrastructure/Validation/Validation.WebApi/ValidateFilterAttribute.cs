// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateFilterAttribute.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    
    using Infrastructure.Interception.Contract;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.Exception;
    using Infrastructure.Exception.ErrorCodes;
    using Infrastructure.Validation.Contracts;

    using Microsoft.Practices.Unity;

    using ValidationResult = SpecExpress.ValidationResult;

    /// <summary>
    ///     The validate filter attribute.
    /// </summary>
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     Gets or sets the dependency resolver.
        /// </summary>
        [Dependency]
        public IDependencyResolver DependencyResolver { get; set; }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        [Dependency]
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Gets or sets the message validator.
        /// </summary>
        //[Dependency]
        public IMessageValidator MessageValidator { get; set; }

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.MessageValidator = (IMessageValidator)actionContext.Request.GetDependencyScope().GetService(typeof(IMessageValidator));
            this.Logger = (ILogger)actionContext.Request.GetDependencyScope().GetService(typeof(ILogger));
            this.DependencyResolver =
                (IDependencyResolver)actionContext.Request.GetDependencyScope().GetService(typeof(IDependencyResolver));

            this.Logger.Debug(() => "ValidateFilterAttribute.OnActionExecuting");

            var validationInstance = actionContext.ActionArguments.Values.FirstOrDefault();
            if (validationInstance == null)
            {
                var errors = new List<ExceptionMessage>();
                errors.Add(new ExceptionMessage(BaseErrorCodes.CommandValidation, "Command should not be null."));
                throw this.DependencyResolver.Resolve<ValidationException>()
                    .GetException(errors, isTopAsRoot: true);
            }

            var validationNotification = this.MessageValidator.Validate(validationInstance);

            if (!actionContext.ModelState.IsValid || !validationNotification.IsValid)
            {
                throw this.DependencyResolver.Resolve<ValidationException>()
                    .GetException(
                        this.BuildNestedMessages(validationNotification.Errors),
                        "Validation failed",
                        BaseErrorCodes.CommandValidation);

                //ObjectContent<List<ValidationResult>> content = new ObjectContent<List<ValidationResult>>(validationNotification.Errors, new JsonMediaTypeFormatter());
                //var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                //{
                //    Content = content,
                //    ReasonPhrase = "Validation failed",
                //    RequestMessage = actionContext.Request
                //};
                //actionContext.Response = resp;
            }
        }

        private List<NestedMessage> BuildNestedMessages(IEnumerable<ValidationResult> validationResults)
        {
            return
                validationResults.Select(
                    v => new NestedMessage { Message = v.Message, NestedMessages = this.BuildNestedMessages(v.NestedValidationResults) })
                    .ToList();
        }
    }
}