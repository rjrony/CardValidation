// --------------------------------------------------------------------------------------------------------------------
// <copyright file="External.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The inner exception message.
    /// </summary>
    public class External : ExceptionErrorMessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="errorMessages">
        /// The error messages.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public External(int status, List<string> errorMessages, string message, object content = null)
            : base(ExceptionErrorMassageType.External)
        {
            this.Status = status;
            this.NestedMessages = errorMessages?.Select(e => new NestedMessage { Message = e }).ToList();
            this.Content = content;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public External(int status, string errorMessage, object content = null)
            : base(ExceptionErrorMassageType.External)
        {
            this.Status = status;
            this.Message = errorMessage;
            //this.ErrorMessages = errorMessages;
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="External"/> class.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <param name="endPoint">
        /// The end point.
        /// </param>
        /// <param name="requestDto">
        /// The request dto.
        /// </param>
        /// <param name="customKeys">
        /// The custom keys.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        public External(
            int status,
            string errorMessage,
            string endPoint,
            object requestDto,
            List<KeyValuePair<string, string>> customKeys = null,
            object content = null)
            : base(ExceptionErrorMassageType.External)
        {
            this.Status = status;
            this.Message = errorMessage;
            this.Content = content;
            this.EndPoint = endPoint;
            this.OccurenceTime = DateTime.Now;
            this.RequestDto = requestDto;
            this.CustomKeys = customKeys;
        }

        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        ///     Gets or sets the custom keys.
        /// </summary>
        public List<KeyValuePair<string, string>> CustomKeys { get; set; }

        /// <summary>
        ///     Gets or sets the end point.
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        ///     Gets or sets the occurence time.
        /// </summary>
        public DateTime OccurenceTime { get; set; }

        /// <summary>
        ///     Gets or sets the request dto.
        /// </summary>
        public object RequestDto { get; set; }

        ///// <summary>
        ///// Gets or sets the error messages.
        ///// </summary>
        //public List<string> ErrorMessages { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public int Status { get; set; }
    }
}