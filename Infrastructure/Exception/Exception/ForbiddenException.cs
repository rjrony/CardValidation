// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForbiddenException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The forbidden response exception.
    /// </summary>
    public class ForbiddenException : BaseException<Exceptions.ForbiddenException>
    {
        private const string DefaultErrorMessage = "ForbiddenException";

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public ForbiddenException()
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
            return this.BuildException(HttpStatusCode.Forbidden, baseErrorCode, message, null);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, List<string> errorList, string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.Forbidden, baseErrorCode, message, errorList);
        }
    }
}