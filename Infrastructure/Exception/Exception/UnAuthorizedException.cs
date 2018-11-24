// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnAuthorizedException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Infrastructure.Exception.ErrorCodes;
    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The un authorized response exception.
    /// </summary>
    public class UnAuthorizedException : BaseException<Exceptions.UnAuthorizedException>
    {
        private const string DefaultErrorMessage = "UnAuthorizedException";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnAuthorizedException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public UnAuthorizedException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(string message = DefaultErrorMessage)
        {
            return this.GetException(new List<string>(), BaseErrorCodes.InvalidInput, message);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(List<string> errorList, string message = DefaultErrorMessage)
        {
            return this.GetException(errorList, BaseErrorCodes.UnAuthorized, message);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(List<string> errorList, Enumeration<int> baseErrorCode, string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.Unauthorized, baseErrorCode, message, errorList);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
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
        public Exception GetException(List<string> errorList, Enumeration<int> baseErrorCode, Exception exception, string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.Unauthorized, baseErrorCode, exception, message, errorList);
        }
    }
}