// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResult.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Infrastructure.Localization.Contracts;

    /// <summary>
    ///     The validation result.
    /// </summary>
    public class ValidationResult : IHandledResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="validationNotification">
        /// The validation notification.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="originalNotification">
        /// The original Notification.
        /// </param>
        /// <param name="isValid">
        /// The is Valid.
        /// </param>
        public ValidationResult(
            IEnumerable<LanguageValue> validationNotification,
            object message,
            object originalNotification,
            bool isValid)
        {
            this.ValidationNotification = validationNotification;
            this.Message = message;
            this.OriginalNotification = originalNotification;
            this.IsValid = isValid;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is handled.
        /// </summary>
        public bool IsHandled { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is valid.
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     Gets the message.
        /// </summary>
        public object Message { get; private set; }

        /// <summary>
        ///     Gets the original notification.
        /// </summary>
        public object OriginalNotification { get; private set; }

        /// <summary>
        ///     Gets the validation notification.
        /// </summary>
        public IEnumerable<LanguageValue> ValidationNotification { get; private set; }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            if (this.OriginalNotification != null)
            {
                return this.OriginalNotification.ToString();
            }

            var currentLanguageValue =
                this.ValidationNotification.FirstOrDefault(l => l.Language.CultureInfo.Equals(Thread.CurrentThread.CurrentCulture));
            if (currentLanguageValue != null)
            {
                return currentLanguageValue.Value;
            }

            currentLanguageValue = this.ValidationNotification.FirstOrDefault();
            if (currentLanguageValue != null)
            {
                return currentLanguageValue.Value;
            }

            return base.ToString();
        }
    }
}