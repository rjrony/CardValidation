// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System.Text.RegularExpressions;

    using SpecExpress;
    using SpecExpress.Rules;

    /// <summary>
    /// The name validator.
    /// </summary>
    /// <typeparam name="T">
    /// The Name string
    /// </typeparam>
    public class NameValidator<T> : RuleValidator<T, string>, IRegexValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValidator{T}" /> class.
        /// </summary>
        public NameValidator()
        {
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.NameWithInvalidCharacters;
        }

        /// <summary>
        ///     Gets the regex pattern.
        /// </summary>
        /// <value>
        ///     The regex pattern.
        /// </value>
        public string RegexPattern
        {
            get
            {
                // The valid name regex taken from eCH-0084-commons-1-3.xsd - baseNameUPI_Type
                // xml:
                // "[']?[A-Za-zÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿŒœŠšŸŽž]['A-Za-zÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿŒœŠšŸŽž\.\- ]*"
                return
                    @"^[']?[A-Za-zÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿŒœŠšŸŽž]['A-Za-zÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿŒœŠšŸŽž\.\- ]*$";
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

            var nameRegex = new Regex(this.RegexPattern);
            var isValid = nameRegex.IsMatch(context.PropertyValue);
            return this.Evaluate(isValid, context, notification);
        }
    }
}