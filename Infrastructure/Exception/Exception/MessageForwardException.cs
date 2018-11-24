// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageForwardException.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Net;


    using Infrastructure.Interception.Contract;

    /// <summary>
    /// The message forward exception.
    /// </summary>
    public class MessageForwardException : BaseException<Exceptions.MessageForwardException>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageForwardException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public MessageForwardException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="exceptionMessage">
        /// The exception message.
        /// </param>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        /// <param name="resonPhrase">
        /// The reson phrase.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(ExceptionMessage exceptionMessage, HttpStatusCode httpStatusCode, string resonPhrase)
        {
            return this.BuildException(httpStatusCode, exceptionMessage, resonPhrase);
        }
    }
}