// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidInputException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The invalid input exception.
    /// </summary>
    public class InvalidInputException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class.
        /// </summary>
        public InvalidInputException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public InvalidInputException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidInputException"/> class.
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
        public InvalidInputException(string message, string stackTrace, IDictionary data)
            : base(message, stackTrace, data)
        {
        }
    }
}