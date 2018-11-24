// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageForwardException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The message forward exception.
    /// </summary>
    public class MessageForwardException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageForwardException"/> class.
        /// </summary>
        public MessageForwardException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageForwardException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public MessageForwardException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MessageForwardException"/> class.
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
        public MessageForwardException(string message, string stackTrace, IDictionary data)
            : base(message, stackTrace, data)
        {
        }
    }
}