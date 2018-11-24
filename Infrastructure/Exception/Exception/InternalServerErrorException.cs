// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalServerErrorException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    ///     The internal server error response exception.
    /// </summary>
    public class InternalServerErrorException : BaseException<Exceptions.InternalServerErrorException>
    {
        private const string DefaultErrorMessage = "InternalServerErrorException";

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public InternalServerErrorException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
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
        public Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage, List<string> errorList = null)
        {
            return this.BuildException(HttpStatusCode.InternalServerError, baseErrorCode, message, errorList);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, Exception exception, string message = DefaultErrorMessage)
        {
            var errorMessageBaseList = new List<ExceptionErrorMessageBase>();
            errorMessageBaseList.Add(new UnhandledExceptionMessage(exception));

            return this.BuildException(
                HttpStatusCode.InternalServerError,
                errorMessageBaseList,
                baseErrorCode,
                message,
                exception);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="showNestedMessage">
        /// The show nested message.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, Exception exception, bool showNestedMessage, string message = DefaultErrorMessage)
        {
            var errorMessageBaseList = new List<ExceptionErrorMessageBase>();

            if (showNestedMessage)
            {
                errorMessageBaseList.Add(new UnhandledExceptionMessage(exception));
            }

            return this.BuildException(
                HttpStatusCode.InternalServerError, 
                errorMessageBaseList, 
                baseErrorCode, 
                message, 
                exception);
        }
    }
}