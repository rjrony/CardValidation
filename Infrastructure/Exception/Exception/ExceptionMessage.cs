// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionMessage.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Exception
{
    using System.Collections.Generic;
    using Infrastructure.DependencyContainerBuilder;

    /// <summary>
    /// The exception message.
    /// </summary>
    public class ExceptionMessage : ExceptionErrorMessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessage"/> class.
        /// </summary>
        public ExceptionMessage()
            : base(ExceptionErrorMassageType.Root)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessage"/> class.
        /// </summary>
        /// <param name="errorCodeValue">
        /// The error code value.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        public ExceptionMessage(Enumeration<int> errorCodeValue, string message, IEnumerable<ExceptionErrorMessageBase> nestedMessages = null)
            : this()
        {
            this.ErrorCodeValue = errorCodeValue;
            this.Message = message;
            this.Type = ExceptionErrorMassageType.Root;
            this.NestedMessages = nestedMessages;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessage"/> class.
        /// </summary>
        /// <param name="errorCodeValue">
        /// The error code value.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="nestedMessages">
        /// The nested messages.
        /// </param>
        public ExceptionMessage(Enumeration<int> errorCodeValue, string message, ExceptionErrorMassageType type, IEnumerable<ExceptionErrorMessageBase> nestedMessages = null)
            : this()
        {
            this.ErrorCodeValue = errorCodeValue;
            this.Message = message;
            this.Type = type;
            this.NestedMessages = nestedMessages;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode
        {
            get
            {
                if (this.ErrorCodeValue == null)
                {
                    return -1;
                }

                return this.ErrorCodeValue.Value;
            }
        }

        /// <summary>
        /// Gets the error code key.
        /// </summary>
        public string ErrorCodeKey
        {
            get
            {
                if (this.ErrorCodeValue == null)
                {
                    return "ErrorCodeValueSet";
                }

                return this.ErrorCodeValue.GetType().Name;
            }
        }

        /// <summary>
        /// Gets the user message.
        /// </summary>
        public string UserMessage
        {
            get
            {
                if (ResourceProvider.ResourceSet.IsNull())
                {
                    return null;
                }

                if (this.ErrorCodeValue == null)
                {
                    return "ErrorCodeValueSet";
                }

                return ResourceProvider.GetResource(this.ErrorCodeValue.GetType().Name);
            }
        }

        /// <summary>
        /// The should serialize user message: don't serialize the UserMessage property if it returns null.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShouldSerializeUserMessage()
        {
            return this.UserMessage != null;
        }

        /// <summary>
        /// Gets or sets the error code value.
        /// </summary>
        public Enumeration<int> ErrorCodeValue { get; set; }
    }
}