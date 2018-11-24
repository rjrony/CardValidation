// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoContentException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The no content exception.
    /// </summary>
    public class NoContentException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        public NoContentException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public NoContentException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
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
        public NoContentException(string message, string stackTrace, IDictionary data) : base(message, stackTrace, data)
        {
        }
    }
}