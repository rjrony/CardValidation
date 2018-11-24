// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnAuthorizedException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The un authorized exception.
    /// </summary>
    public class UnAuthorizedException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnAuthorizedException"/> class.
        /// </summary>
        public UnAuthorizedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnAuthorizedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public UnAuthorizedException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnAuthorizedException"/> class.
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
        public UnAuthorizedException(string message, string stackTrace, IDictionary data)
            : base(message, stackTrace, data)
        {
        }
    }
}