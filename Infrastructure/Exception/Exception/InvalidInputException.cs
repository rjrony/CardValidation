// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidInputException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;

    using Infrastructure.Exception.ErrorCodes;
    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The invalid input exception.
    /// </summary>
    public class InvalidInputException : BaseException<Exceptions.InvalidInputException>
    {
        private const string DefaultErrorMessage = "InvalidInputException";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public InvalidInputException()
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
            return this.GetException(new List<ExceptionMessage>(), BaseErrorCodes.InvalidInput, message);
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
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage)
        {
            return this.GetException(new List<ExceptionMessage>(), baseErrorCode, message);
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
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage, IDictionary data = null)
        {
            return this.GetException(new List<ExceptionMessage>(), baseErrorCode, message, data);
        }

        private Exception GetException(List<ExceptionMessage> errorList, Enumeration<int> baseErrorCode, string message = DefaultErrorMessage, IDictionary data = null)
        {
            return this.BuildException(HttpStatusCode.BadRequest, errorList, baseErrorCode, message, data);
        }
    }
}