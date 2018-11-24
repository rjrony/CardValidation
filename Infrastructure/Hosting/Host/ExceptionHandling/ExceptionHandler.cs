// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Host.ExceptionHandling
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Web.Http.ExceptionHandling;
    using global::Host.Owin;
    using global::Infrastructure;
    using global::Infrastructure.Logging.Contracts;

    using Infrastructure.Exception;
    using Infrastructure.Exception.ErrorCodes;

    using IExceptionHandler = Infrastructure.Host.Contracts.IExceptionHandler;

    /// <summary>
    ///     The exception handler.
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        public void HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var logger = (ILogger)context.Request.GetDependencyScope().GetService(typeof(ILogger));
            logger.Exception(context.Exception);

            InternalServerErrorException internalServerErrorException = (InternalServerErrorException)context.Request.GetDependencyScope();
                
            internalServerErrorException.GetException(BaseErrorCodes.UnhandledException, context.Exception);

            var messageToReturn = internalServerErrorException.Content;

            // var messageToReturn = "Something went wrong on the server";

            ////For the time being we are reading the app setting variable directly from appsettings since environment library is not stable.
            // TODO:must have to read the appsetting through Environment Library.           
            var enableErrorDetails = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableErrorDetails"])
                                     && Convert.ToBoolean(ConfigurationManager.AppSettings["EnableErrorDetails"]);

            //if (EnvironmentInfo.IsDevelopmentMachine || enableErrorDetails)
            //{
            //    var sb = new StringBuilder();
            //    sb.AppendLine();
            //    sb.AppendLine();
            //    sb.AppendLine("------------------- Development Environment ----------------------");
            //    sb.AppendLine(context.Exception.GetType().FullName);
            //    sb.AppendLine(this.GetMessageToReturn(context.Exception, Environment.NewLine));
            //    sb.AppendLine(context.Exception.StackTrace);

            //    messageToReturn = sb.ToString();
            //}

            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is NotImplementedException)
            {
                statusCode = HttpStatusCode.NotImplemented;
            }

            /*
            else if (context.Exception is AuthorizationException)
            {
                statusCode = HttpStatusCode.Forbidden;
            }
            else if (context.Exception is InvalidInputException)
            {
                var exception = (InvalidInputException)context.Exception;

                ObjectContent<List<string>> content = new ObjectContent<List<string>>(exception.Errors, new JsonMediaTypeFormatter());
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = content,
                    ReasonPhrase = "Invalid Input",
                    RequestMessage = context.ExceptionContext.Request
                };
                context.Result = new ErrorMessageResult(response);
                return;
            }
            else if (context.Exception is ExternalRequestException)
            {
                var exception = (ExternalRequestException)context.Exception;
                
                /*
                statusCode = exception.ExternalResult.StatusCode;
                var response = new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(exception.Content),
                    ReasonPhrase = "Bad Request",
                    RequestMessage = context.ExceptionContext.Request
                };
                

                context.Result = new ErrorMessageResult(exception.ExternalResult);
                return;
            }
            */

            // return the errors back to the caller
            // context.Response = useHttpError
            // ? context.Request.CreateErrorResponse(statusCode, messageToReturn)
            // : context.Request.CreateResponse(statusCode);
            var resp = new HttpResponseMessage(statusCode)
            {
                Content = messageToReturn,
                ReasonPhrase = context.Exception.GetType().FullName,
                RequestMessage = context.ExceptionContext.Request
            };

            context.Result = new ErrorMessageResult(resp);
        }

        #region Methods

        /// <summary>
        /// The get message to return.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="delimitter">
        /// The delimitter.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetMessageToReturn(Exception exception, string delimitter = " ")
        {
            if (exception == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            sb.Append(exception.Message + delimitter);
            if (exception.InnerException == null)
            {
                return sb.ToString().Trim();
            }

            return sb.Append((this.GetMessageToReturn(exception.InnerException, delimitter) + delimitter).Trim()).ToString().Trim();
        }

        #endregion
    }
}