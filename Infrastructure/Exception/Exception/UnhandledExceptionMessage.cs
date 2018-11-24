// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnhandledExceptionMessage.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    ///     The unhandled exception message.
    /// </summary>
    public class UnhandledExceptionMessage : ExceptionErrorMessageBase
    {
        public Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionMessage"/> class.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public UnhandledExceptionMessage(Exception exception)
            : base(ExceptionErrorMassageType.Root)
        {
            this.exception = exception;
        }
    }
}