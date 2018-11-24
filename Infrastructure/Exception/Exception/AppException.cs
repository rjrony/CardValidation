// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppException.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Net;
using System.Net.Http;

namespace Infrastructure.Exception
{
    /// <summary>
    ///     The app exception.
    /// </summary>
    public class AppException : System.Exception
    {
        private readonly string _innerStackTrace = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        public AppException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="stackTrace">
        /// The stack trace.
        /// </param>
        public AppException(string message, string stackTrace)
            : base(message)
        {
            this._innerStackTrace = stackTrace;
        }

        public AppException(string message, string stackTrace, IDictionary data)
            : base(message)
        {
            this._innerStackTrace = stackTrace;
            if (data != null && data.Keys.Count > 0)
            {
                foreach (var key in data.Keys)
                {
                    this.Data.Add(key, data[key]);
                }
            }            
        }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public ObjectContent Content { get; set; }

        /// <summary>
        ///     Gets or sets the http status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        ///     Gets or sets the reason phrase.
        /// </summary>
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        public override string StackTrace
        {
            get
            {
                return this._innerStackTrace;
            }
        }
    }
}