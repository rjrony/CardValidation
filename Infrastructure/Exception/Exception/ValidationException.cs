// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The validation exception.
    /// </summary>
    public class ValidationException : BaseException<Exceptions.ValidationException>
    {
        private const string DefaultErrorMessage = "ValidationException";

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public ValidationException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="nestedExceptionMessages">
        /// The nested exception messages.
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
            IEnumerable<ExceptionErrorMessageBase> nestedExceptionMessages, 
            Enumeration<int> baseErrorCode, 
            string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.BadRequest, nestedExceptionMessages.ToList(), baseErrorCode, message);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="nestedExceptionMessages">
        /// The nested exception messages.
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
            IEnumerable<ExceptionErrorMessageBase> nestedExceptionMessages, 
            string reason, 
            Enumeration<int> baseErrorCode, 
            string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.BadRequest, nestedExceptionMessages.ToList(), baseErrorCode, message, reason);
        }
    }
}