// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionErrorMessageBase.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// <summary>
//   The exception error message base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Exception
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    ///     The exception error message base.
    /// </summary>
    public abstract class ExceptionErrorMessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionErrorMessageBase"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public ExceptionErrorMessageBase(ExceptionErrorMassageType type)
        {
            this.Type = type;
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the nested messages.
        /// </summary>
        public IEnumerable<ExceptionErrorMessageBase> NestedMessages { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public ExceptionErrorMassageType Type { get; set; }

        /// <summary>
        /// Gets or sets is detail exposable
        /// </summary>
        [JsonIgnore]
        public bool IsDetailExposable { get; set; } = true;
        
        /// <summary>
        /// The should serialize nested messages.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShouldSerializeNestedMessages()
        {
            return this.IsDetailExposable;
        }

        public IDictionary Data { get; set; }

        public string CorrelationId { get; set; }
    }
}