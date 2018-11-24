// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Interception;
using Newtonsoft.Json;
using Microsoft.Practices.Unity;

namespace Infrastructure.Exception
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;


    using Infrastructure.Exception.ErrorCodes;
    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The base http response exception.
    /// </summary>
    public abstract class BaseException<T> where T : AppException, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public BaseException()
        {
        }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public ObjectContent Content { get; set; }

        /// <summary>
        ///     Gets the exception.
        /// </summary>
        /// <summary>
        ///     Gets or sets the http status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }


        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="isTopAsRoot">
        /// The is top as root.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(List<ExceptionMessage> errorList, string message = "exception", bool isTopAsRoot = false)
        {
            if (errorList != null && errorList.Count > 0)
            {
                return isTopAsRoot ?
                           this.BuildException(HttpStatusCode.BadRequest,
                               errorList.Count == 1 ? null : errorList.Skip(1).ToList(),
                               errorList[0].ErrorCodeValue,
                               errorList[0].Message)
                           : this.BuildException(HttpStatusCode.BadRequest, errorList, BaseErrorCodes.InvalidInput, message);
            }

            var exceptionResponse = new ExceptionMessage
            {
                Message = "No item found in exception list",
                NestedMessages = null,
                ErrorCodeValue = BaseErrorCodes.InvalidInput,
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(HttpStatusCode.BadRequest, this.Content);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="orginalException">
        /// The orginal exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="isTopAsRoot">
        /// The is top as root.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(List<ExceptionMessage> errorList, Exception orginalException, string message = "exception", bool isTopAsRoot = false)
        {
            if (errorList != null && errorList.Count > 0)
            {
                return isTopAsRoot ?
                           this.BuildException(HttpStatusCode.BadRequest,
                               errorList.Count == 1 ? null : errorList.Skip(1).ToList(),
                               errorList[0].ErrorCodeValue,
                               errorList[0].Message, orginalException)
                           : this.BuildException(HttpStatusCode.BadRequest, errorList, BaseErrorCodes.InvalidInput, message, orginalException);
            }

            var exceptionResponse = new ExceptionMessage
            {
                Message = "No item found in exception list",
                NestedMessages = null,
                ErrorCodeValue = BaseErrorCodes.InvalidInput
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(HttpStatusCode.BadRequest, this.Content, orginalException);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            Enumeration<int> baseErrorCode,
            string message = "exception",
            List<string> errorList = null)
        {
            var exceptionResponse = new ExceptionMessage
            {
                Message = message,
                NestedMessages = errorList?.Select(e => new NestedMessage { Message = e }).ToList(),
                ErrorCodeValue = baseErrorCode,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="orginalException">
        /// The orginal exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            Enumeration<int> baseErrorCode,
            Exception orginalException,
            string message = "exception",
            List<string> errorList = null)
        {
            var exceptionResponse = new ExceptionMessage
            {
                Message = message,
                NestedMessages = errorList?.Select(e => new NestedMessage { Message = e }).ToList(),
                ErrorCodeValue = baseErrorCode,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content, orginalException);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            Enumeration<int> baseErrorCode,
            string reason,
            string message = "exception",
            List<string> errorList = null)
        {
            var exceptionResponse = new ExceptionMessage
            {
                Message = message,
                NestedMessages =
                                                errorList?.Select(e => new NestedMessage { Message = e }).ToList(),
                ErrorCodeValue = baseErrorCode,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content, reason);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            IEnumerable<ExceptionErrorMessageBase> nestedMessages,
            Enumeration<int> baseErrorCode,
            string message, IDictionary data = null)
        {
            var exceptionResponse = new ExceptionMessage
            {
                NestedMessages = nestedMessages,
                ErrorCodeValue = baseErrorCode,
                Message = message,
                Data = data,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());

            return this.Generate(statusCode, this.Content, data);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="orGinalException">
        /// The or ginal exception.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            IEnumerable<ExceptionErrorMessageBase> nestedMessages,
            Enumeration<int> baseErrorCode,
            string message,
            Exception orGinalException)
        {
            var exceptionResponse = new ExceptionMessage
            {
                NestedMessages = nestedMessages,
                ErrorCodeValue = baseErrorCode,
                Message = message,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content, orGinalException);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            IEnumerable<ExceptionErrorMessageBase> nestedMessages,
            Enumeration<int> baseErrorCode,
            string message,
            string reason)
        {
            var exceptionResponse = new ExceptionMessage
            {
                NestedMessages = nestedMessages,
                ErrorCodeValue = baseErrorCode,
                Message = message,
                CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId
            };
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionResponse, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content, reason);
        }

        /// <summary>
        /// The build exception.
        /// </summary>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        /// <param name="exceptionMessage">
        /// The exception message.
        /// </param>
        /// <param name="reason">
        /// The reason.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        protected Exception BuildException(
            HttpStatusCode statusCode,
            ExceptionMessage exceptionMessage,
            string reason)
        {
            exceptionMessage.CorrelationId = ServiceLocator.Instance.Current.Resolve<IOperationContext>().CorrelationId;
            this.Content = new ObjectContent(typeof(ExceptionMessage), exceptionMessage, new JsonMediaTypeFormatter());
            return this.Generate(statusCode, this.Content, reason);
        }



        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="objectContent">
        /// The object content.
        /// </param>
        /// <param name="data"></param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception Generate(HttpStatusCode httpStatusCode, ObjectContent objectContent, IDictionary data = null)
        {
            string appExceptionMessage = "";
            if (objectContent.Value is ExceptionMessage)
            {
                ExceptionMessage exceptionMessage = objectContent.Value as ExceptionMessage;

                appExceptionMessage = exceptionMessage.Message;
            }

            var exception = Activator.CreateInstance(typeof(T), appExceptionMessage, "NoStackTrace", data) as T;
            exception.HttpStatusCode = httpStatusCode;
            exception.Content = objectContent;

            return exception;

            //  var exception = new AppException(appExceptionMessage, "NoStackTrace", data) { HttpStatusCode = httpStatusCode, Content = objectContent };
            //  return exception;
        }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="objectContent">
        /// The object content.
        /// </param>
        /// <param name="orginalException">
        /// The orginal exception.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception Generate(HttpStatusCode httpStatusCode, ObjectContent objectContent, Exception orginalException)
        {

            string orginalExceptionMsg = "", orginalExceptionStackTrace = "";
            this.ExtractOrginalExceptionContent(orginalException, ref orginalExceptionMsg, ref orginalExceptionStackTrace);

           /* if (objectContent.Value is ExceptionMessage)
            {
                ExceptionMessage exceptionMessage = objectContent.Value as ExceptionMessage;
                //var debug =$"A AppException is Initiated with HttpStatusCode: {httpStatusCode}, \nMessage: {exceptionMessage.Message}, ErrorCode: {exceptionMessage.ErrorCode}, \nOrginalException: {JsonConvert.SerializeObject(orginalException, Formatting.Indented, new JsonSerializerSettings { MaxDepth = 5 })}\n";
                
            }*/

            var exception = Activator.CreateInstance(typeof(T), orginalExceptionMsg, orginalExceptionStackTrace) as T;
            exception.HttpStatusCode = httpStatusCode;
            exception.Content = objectContent;

            //var exception = new AppException(orginalExceptionMsg, orginalExceptionStackTrace)
            //{
            //    HttpStatusCode = httpStatusCode,
            //    Content = objectContent
            //};


            return exception;
        }


        private void ExtractOrginalExceptionContent(Exception orginalException, ref string message, ref string stacktrace)
        {
            message += orginalException.Message;
            stacktrace += orginalException.StackTrace;

            if (orginalException.InnerException != null)
            {
                message += "\nInnerExceptionMessage: ";
                stacktrace += "\nInnerExceptionstacktrace: ";
                this.ExtractOrginalExceptionContent(orginalException.InnerException, ref message, ref stacktrace);
            }

        }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="objectContent">
        /// The object content.
        /// </param>
        /// <param name="reasonPhrase">
        /// The reason phrase.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception Generate(HttpStatusCode httpStatusCode, ObjectContent objectContent, string reasonPhrase)
        {
            string appExceptionMessage = "";
            if (objectContent.Value is ExceptionMessage)
            {
                ExceptionMessage exceptionMessage = objectContent.Value as ExceptionMessage;

                appExceptionMessage = exceptionMessage.Message;
            }

            var exception = Activator.CreateInstance(typeof(T), appExceptionMessage, "NoStackTrace") as T;
            exception.HttpStatusCode = httpStatusCode;
            exception.Content = objectContent;
            exception.ReasonPhrase = reasonPhrase;

            //   var exception = new AppException(appExceptionMessage, "NoStackTrace") { HttpStatusCode = httpStatusCode, ReasonPhrase = reasonPhrase, Content = objectContent };
            return exception;
        }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="objectContent">
        /// The object content.
        /// </param>
        /// <param name="reasonPhrase">
        /// The reason phrase.
        /// </param>
        /// <param name="orginalException">
        /// The orginal exception.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception Generate(
            HttpStatusCode httpStatusCode,
            ObjectContent objectContent,
            string reasonPhrase,
            Exception orginalException)
        {
            string orginalExceptionMsg = "", orginalExceptionStackTrace = "";
            this.ExtractOrginalExceptionContent(orginalException, ref orginalExceptionMsg, ref orginalExceptionStackTrace);

            if (objectContent.Value is ExceptionMessage)
            {
                ExceptionMessage exceptionMessage = objectContent.Value as ExceptionMessage;
            }


            var exception = Activator.CreateInstance(typeof(T), orginalExceptionMsg, orginalExceptionStackTrace) as T;
            exception.HttpStatusCode = httpStatusCode;
            exception.Content = objectContent;



            //var exception = new AppException(orginalExceptionMsg, orginalExceptionStackTrace)
            //{
            //    HttpStatusCode = httpStatusCode,
            //    Content = objectContent
            //};
            return exception;
        }




        public static string ToDebugString(IDictionary dictionary)
        {
            return JsonConvert.SerializeObject(dictionary, Formatting.Indented, new JsonSerializerSettings { MaxDepth = 3 });

        }
    }
}