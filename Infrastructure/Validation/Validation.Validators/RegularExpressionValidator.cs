// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegularExpressionValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    using SpecExpress;
    using SpecExpress.Rules;

    /// <summary>
    /// The email address validator.
    /// </summary>
    /// <typeparam name="T">
    /// The email address string
    /// </typeparam>
    public class RegularExpressionValidator<T> : RuleValidator<T, string>, IRegexValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegularExpressionValidator{T}"/> class.
        /// </summary>
        /// <param name="regexPattern">
        /// The regex Pattern.
        /// </param>
        public RegularExpressionValidator(string regexPattern)
        {
            this.RegexPattern = regexPattern;
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.InvalidRegularExpression;
        }

        /// <summary>
        /// Gets the regex pattern.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml",
            Justification = "Reviewed. Suppression is OK here.")]
        public string RegexPattern { get; }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="specificationContainer">
        /// The specification container.
        /// </param>
        /// <param name="notification">
        /// The notification.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Validate(
            RuleValidatorContext<T, string> context,
            SpecificationContainer specificationContainer,
            ValidationNotification notification)
        {
            if (string.IsNullOrEmpty(context.PropertyValue))
            {
                return false;
            }

            var emailRegex = new Regex(this.RegexPattern);
            var isValid = emailRegex.IsMatch(context.PropertyValue);
            return this.Evaluate(isValid, context, notification);
        }
    }
}