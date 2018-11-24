// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalRequestException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The external request exception.
    /// </summary>
    public class ExternalRequestException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalRequestException"/> class.
        /// </summary>
        public ExternalRequestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalRequestException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public ExternalRequestException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalRequestException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public ExternalRequestException(string message, string stackTrace, IDictionary data)
            : base(message, stackTrace, data)
        {
        }
    }
}