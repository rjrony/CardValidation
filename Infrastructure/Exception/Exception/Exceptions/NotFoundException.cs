// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotFoundException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;

namespace Infrastructure.Exception.Exceptions
{
    /// <summary>
    /// The not found exception.
    /// </summary>
    public class NotFoundException : AppException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public NotFoundException(string message, string stackTrace) : base(message, stackTrace)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
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
        public NotFoundException(string message, string stackTrace, IDictionary data) : base(message, stackTrace, data)
        {
        }
    }
}