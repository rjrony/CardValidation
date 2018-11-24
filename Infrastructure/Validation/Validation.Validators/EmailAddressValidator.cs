// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailAddressValidator.cs">
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
    public class EmailAddressValidator<T> : RuleValidator<T, string>, IRegexValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailAddressValidator{T}" /> class.
        /// </summary>
        public EmailAddressValidator()
        {
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.InvalidEmailAddress;
        }

        /// <summary>
        /// Gets the regex pattern.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1603:DocumentationMustContainValidXml",
            Justification = "Reviewed. Suppression is OK here.")]
        public string RegexPattern
        {
            get
            {
                return
                    @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$";
            }
        }

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