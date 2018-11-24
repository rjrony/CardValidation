// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForbiddenException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The forbidden exception.
    /// </summary>
    public class ForbiddenException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        public ForbiddenException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public ForbiddenException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
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
        public ForbiddenException(string message, string stackTrace, IDictionary data) : base(message, stackTrace, data)
        {
        }
    }
}