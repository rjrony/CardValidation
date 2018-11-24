// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    
    using Infrastructure.Localization.Contracts;
    using Infrastructure.Logging.Contracts;

    using Infrastructure.Interception.Contract;
    using Infrastructure.Validation.Contracts;

    using SpecExpress;

    using ValidationResult = Infrastructure.Validation.Contracts.ValidationResult;

    /// <summary>
    ///     The message validator.
    /// </summary>
    public class MessageValidator : IMessageValidator
    {
        private readonly IDependencyResolver resolver;

        private ILogger logger;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageValidator"/> class.
        /// </summary>
        /// <param name="resolver">
        /// Resolver
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public MessageValidator(IDependencyResolver resolver, ILogger logger)
        {
            this.resolver = resolver;

            //this.LanguageSupport = languageSupport;
            this.logger = logger;
        }

        #endregion

        /// <summary>
        ///     Gets or sets the language support.
        /// </summary>
        //[Dependency]//ToDo this have undo again
        public ILanguageSupport LanguageSupport { get; set; }

        /// <summary>
        /// The to errors message.
        /// </summary>
        /// <param name="validationNotification">
        /// The validation notification.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToErrorMessage(ValidationNotification validationNotification)
        {
            var sb = new StringBuilder();
            foreach (var validationResult in validationNotification.Errors)
            {
                sb.Append(string.Join(Environment.NewLine, validationResult.AllErrorMessages().ToArray()));
            }

            return sb.ToString();
        }

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
        public ValidationNotification Validate(object toValidationInstance)
        {
            if (toValidationInstance == null)
            {
                throw new ArgumentNullException("toValidationInstance");
            }

            if (this.logger == null)
            {
                this.logger = new NullLogger();
            }

            var messageType = toValidationInstance.GetMessageType();

            this.logger.Debug(() => "Unity Of Work Id: {0}".FormatWith(this.resolver.GetHashCode()));
            ValidationContextUnitOfWork.SetUnitOfWorkId(this.resolver);
            var spec = ValidationCatalog<ValidationContextUnitOfWork>.SpecificationContainer.TryGetSpecification(messageType);
            if (spec == null)
            {
                this.logger.Debug(() => string.Format("no specifications found for {0}", messageType.FullName));
                return new ValidationNotification();
            }

            this.logger.Debug(() => string.Format("Validating {0}", messageType.FullName));

            var validationResults = ValidationCatalog<ValidationContextUnitOfWork>.Validate(toValidationInstance);

            if (validationResults.IsValid)
            {
                this.logger.Debug(() => string.Format("Validation succeeded for message: {0}", messageType.FullName));
                return validationResults;
            }

            var errorMessage = new StringBuilder();
            errorMessage.Append(
                string.Format(
                    "Validation failed for message {0}, with the following errors/s: " + Environment.NewLine,
                    messageType.FullName));

            foreach (var validationResult in validationResults.Errors)
            {
                errorMessage.Append(string.Join(Environment.NewLine, validationResult.AllErrorMessages().ToArray()));
            }

            this.logger.Debug(this.ToErrorMessage(validationResults).ToString);

            return validationResults;
        }

        /// <summary>
        /// The validate using all supported languages.
        /// </summary>
        /// <param name="toValidationInstance">
        /// The to validation instance.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public ValidationResult ValidateUsingAllSupportedLanguages(object toValidationInstance)
        {
            return this.ValidateUsingLanguage(toValidationInstance, this.LanguageSupport.GetAllLanguages());
        }

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
        public ValidationResult ValidateUsingLanguage(object toValidationInstance, IEnumerable<ILanguage> languages)
        {
            var languageValues = new List<LanguageValue>();
            object validationNotification = null;
            object currentLanguageNotification = null;
            var isValidAndNoMessages = false;
            ValidationResult validationResult = null;

            foreach (var language in languages)
            {
                if (validationResult == null || !isValidAndNoMessages)
                {
                    validationResult = this.ValidateUsingLanguage(toValidationInstance, language);
                    var hasMessages = validationResult.OriginalNotification != null
                                      && ((ValidationNotification)validationResult.OriginalNotification).All().Any();

                    isValidAndNoMessages = validationResult.IsValid && !hasMessages;
                }
                else
                {
                    validationResult = this.CreateValidationResult(
                        toValidationInstance,
                        language,
                        (ValidationNotification)validationResult.OriginalNotification);
                }

                validationNotification = validationResult.OriginalNotification;
                if (language.CultureInfo.Equals(Thread.CurrentThread.CurrentCulture))
                {
                    currentLanguageNotification = validationResult.OriginalNotification;
                }

                languageValues.Add(validationResult.ValidationNotification.First());
            }

            currentLanguageNotification = currentLanguageNotification ?? validationNotification;

            var isValid = currentLanguageNotification == null || ((ValidationNotification)currentLanguageNotification).IsValid;

            return new ValidationResult(languageValues, toValidationInstance, currentLanguageNotification, isValid);
        }

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
        public ValidationResult ValidateUsingLanguage(object toValidationInstance, ILanguage language)
        {
            ValidationNotification validationNotification = null;

            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var currentUiCulture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = language.CultureInfo;
                Thread.CurrentThread.CurrentUICulture = language.CultureInfo;
                validationNotification = this.Validate(toValidationInstance);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
                Thread.CurrentThread.CurrentUICulture = currentUiCulture;
            }

            return this.CreateValidationResult(toValidationInstance, language, validationNotification);
        }

        private ValidationResult CreateValidationResult(
            object toValidationInstance,
            ILanguage language,
            ValidationNotification validationNotification)
        {
            var languageValue = new LanguageValue
                                    {
                                        Language = language,
                                        Value =
                                            validationNotification.Errors.Count > 0
                                                ? validationNotification.ToString()
                                                : string.Empty
                                    };
            return new ValidationResult(
                new[] { languageValue },
                toValidationInstance,
                validationNotification,
                validationNotification.IsValid);
        }
    }
}