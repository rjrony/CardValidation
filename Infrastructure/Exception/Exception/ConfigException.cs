// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidInputException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Infrastructure.Exception.ErrorCodes;
using Infrastructure.Interception.Contract;

namespace Infrastructure.Exception
{
    /// <summary>
    ///     The invalid input exception.
    /// </summary>
    public class ConfigException : BaseException<Exceptions.ConfigException>
    {
        private const string DefaultErrorMessage = "ConfigException";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public ConfigException()
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
        public System.Exception GetException(string message = DefaultErrorMessage)
        {
            return this.GetException(new List<ExceptionMessage>(), BaseErrorCodes.Config, message);
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
        public System.Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage)
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
        public System.Exception GetException(Enumeration<int> baseErrorCode, string message = DefaultErrorMessage, 
            IDictionary data = null)
        {
            return this.GetException(new List<ExceptionMessage>(), baseErrorCode, message, data);
        }

        private System.Exception GetException(List<ExceptionMessage> errorList, Enumeration<int> baseErrorCode, 
            string message = DefaultErrorMessage, IDictionary data = null)
        {
            return this.BuildException(HttpStatusCode.BadRequest, errorList, baseErrorCode, message, data);
        }
    }
}