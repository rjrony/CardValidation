// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadRequestException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Infrastructure.Exception.Exceptions;

namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Infrastructure.Interception.Contract;


    /// <summary>
    ///     The bad request response exception.
    /// </summary>
    public class BadRequestException : BaseException<Exceptions.BadRequestException>
    {
        private const string DefaultErrorMessage = "BadRequestException";

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public BadRequestException()
        {
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
        public Exception GetException(Enumeration<int> baseErrorCode, List<string> errorList = null, string message = DefaultErrorMessage)
        {
            return this.BuildException(HttpStatusCode.BadRequest, baseErrorCode, message, errorList);
        }
    }
}