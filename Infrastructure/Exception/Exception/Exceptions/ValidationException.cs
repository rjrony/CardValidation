// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The validation exception.
    /// </summary>
    public class ValidationException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public ValidationException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
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
        public ValidationException(string message, string stackTrace, IDictionary data)
            : base(message, stackTrace, data)
        {
        }
    }
}