// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalRequestException.cs" company="">
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
    ///     The external request response exception.
    /// </summary>
    public class ExternalRequestException : BaseException<Exceptions.ExternalRequestException>
    {
        private const string DefaultErrorMessage = "ExternalRequestException";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalRequestException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public ExternalRequestException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="external">
        /// The external.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(External external, string message = DefaultErrorMessage)
        {
            return this.GetException(BaseErrorCodes.External, external, message);
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="external">
        /// The external.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode, External external, string message = DefaultErrorMessage)
        {
            return this.BuildException(
                HttpStatusCode.InternalServerError, 
                new List<ExceptionErrorMessageBase> { external }, 
                baseErrorCode, 
                message);
        }
    }
}