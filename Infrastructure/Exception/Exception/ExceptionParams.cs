// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionParams.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    using System.Collections.Generic;

    /// <summary>
    ///     The exception params.
    /// </summary>
    public class ExceptionParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionParams"/> class.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        /// <param name="InnerException">
        /// The inner exception.
        /// </param>
        public ExceptionParams(List<string> errorList, BaseErrorCode baseErrorCode, External InnerException)
        {
            this.ErrorList = errorList;
            this.BaseErrorCode = baseErrorCode;
            this.InnerException = InnerException;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionParams"/> class.
        /// </summary>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        /// <param name="baseErrorCode">
        /// The base error code.
        /// </param>
        public ExceptionParams(List<string> errorList, BaseErrorCode baseErrorCode)
        {
            this.ErrorList = errorList;
            this.BaseErrorCode = baseErrorCode;
        }

        /// <summary>
        ///     Gets or sets the base error code.
        /// </summary>
        public BaseErrorCode BaseErrorCode { get; set; }

        /// <summary>
        ///     Gets or sets the error list.
        /// </summary>
        public List<string> ErrorList { get; set; }

        /// <summary>
        ///     Gets or sets the inner exception.
        /// </summary>
        public External InnerException { get; set; }
    }
}