// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedExceptionMessage.cs" company="">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System.Collections.Generic;

    /// <summary>
    /// The nested exception message.
    /// </summary>
    public class NestedExceptionMessage : ExceptionMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NestedExceptionMessage"/> class.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        public NestedExceptionMessage(Enumeration<int> errorCode, string message, IEnumerable<ExceptionErrorMessageBase> nestedMessages = null)
            : base(errorCode, message, ExceptionErrorMassageType.Nested, nestedMessages)
        {
        }
    }
}