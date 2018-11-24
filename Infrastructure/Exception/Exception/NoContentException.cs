// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoContentException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;
    using System.Net;

    using Infrastructure.Interception.Contract;

    /// <summary>
    ///     The no content response exception.
    /// </summary>
    public class NoContentException : BaseException<Exceptions.NoContentException>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        /// <param name="dependency">
        /// The dependency.
        /// </param>
        public NoContentException()
        {
        }

        /// <summary>
        /// The get exception.
        /// </summary>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <returns>
        /// The <see cref="Exception"/>.
        /// </returns>
        public Exception GetException(Enumeration<int> baseErrorCode)
        {
            return this.BuildException(HttpStatusCode.NoContent, baseErrorCode);
        }
    }
}