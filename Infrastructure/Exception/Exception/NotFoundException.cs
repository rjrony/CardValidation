// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotFoundException.cs" company="">
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
    ///     The not found response exception.
    /// </summary>
    public class NotFoundException : BaseException<Exceptions.NotFoundException>
    {
        private const string DefaultErrorMessage = "NotFoundException";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public NotFoundException()
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
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.NotFound, baseErrorCode, message, null);
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
            return this.BuildException(HttpStatusCode.NotFound, BaseErrorCodes.ItemNotFound, message, null);
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
            return this.BuildException(HttpStatusCode.NotFound, baseErrorCode, message, errorList);
        }
    }
}