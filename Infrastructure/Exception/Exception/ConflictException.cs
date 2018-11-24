// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConflictException.cs" company="">
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
    ///     The conflict response exception.
    /// </summary>
    public class ConflictException : BaseException<Exceptions.ConflictException>
    {
        private const string DefaultErrorMessage = "ForbiddenException";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public ConflictException()
        {
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
            return this.GetException(errorList, BaseErrorCodes.Conflict, message);
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
            return this.BuildException(HttpStatusCode.Conflict, baseErrorCode, message, errorList);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="reason">
        /// The reason.
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
        public Exception GetException(
            List<string> errorList, 
            string reason, 
            Enumeration<int> baseErrorCode, 
            string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.Conflict, baseErrorCode, reason, message, errorList);
        }
    }
}