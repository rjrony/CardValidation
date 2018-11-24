// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Contracts
{
    using System.Collections.Generic;

    using Infrastructure.Localization.Contracts;

    using SpecExpress;

    /// <summary>
    ///     The MessageValidator interface.
    /// </summary>
    public interface IMessageValidator
    {
        /// <summary>
        /// The to error message.
        /// </summary>
        /// <param name="validationNotification">
        /// The validation notification.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string ToErrorMessage(ValidationNotification validationNotification);

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="toValidationInstance">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationNotification"/>.
        /// </returns>
        /// <exception cref="CommandValidationException">
        /// If the message is not valid the exception will be thrown
        /// </exception>
        ValidationNotification Validate(object toValidationInstance);

        /// <summary>
        /// The validate using all supported languages.
        /// </summary>
        /// <param name="toValidationInstance">
        /// The to validation instance.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        ValidationResult ValidateUsingAllSupportedLanguages(object toValidationInstance);

        /// <summary>
        /// The validate using language.
        /// </summary>
        /// <param name="toValidationInstance">
        /// The to validation instance.
        /// </param>
        /// <param name="languages">
        /// The languages.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        ValidationResult ValidateUsingLanguage(object toValidationInstance, IEnumerable<ILanguage> languages);

        /// <summary>
        /// The validate using language.
        /// </summary>
        /// <param name="toValidationInstance">
        /// The to validation instance.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        ValidationResult ValidateUsingLanguage(object toValidationInstance, ILanguage language);
    }
}