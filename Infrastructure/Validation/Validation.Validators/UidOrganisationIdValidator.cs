// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UidOrganisationIdValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System.Globalization;

    using SpecExpress;
    using SpecExpress.Rules;

    /// <summary>
    /// The uid organisation id validator.
    /// </summary>
    /// <typeparam name="T">
    /// Int uid number between 1 and 999 999 999
    /// </typeparam>
    public class UidOrganisationIdValidator<T> : RuleValidator<T, int>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UidOrganisationIdValidator{T}" /> class.
        /// </summary>
        public UidOrganisationIdValidator()
        {
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.InvalidUidNumber;
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
            RuleValidatorContext<T, int> context,
            SpecificationContainer specificationContainer,
            ValidationNotification notification)
        {
            var id = context.PropertyValue;
            if (id <= 0 || id > 999999999)
            {
                return this.Evaluate(false, context, notification);
            }

            // fill with leading zeros to a length of 9 digits
            var idString = id.ToString("000000000");

            // define the multiplicator
            const string MultiplicatorString = "54327654";

            // create integer arrays for multiplication
            var idDigits = new int[idString.Length];
            for (var i = 0; i < idString.Length; i++)
            {
                idDigits[i] = int.Parse(idString[i].ToString(CultureInfo.InvariantCulture));
            }

            var multiplicatorDigits = new int[MultiplicatorString.Length];
            for (var i = 0; i < MultiplicatorString.Length; i++)
            {
                multiplicatorDigits[i] = int.Parse(MultiplicatorString[i].ToString(CultureInfo.InvariantCulture));
            }

            // Generate the product sum
            var productSum = 0;
            for (var i = 0; i < MultiplicatorString.Length; i++)
            {
                productSum += idDigits[i] * multiplicatorDigits[i];
            }

            // modulo 11
            var result = productSum % 11;

            // Check the result: if result is 1, the checkdigit as 11 minus result would be 10. Such a number is not used, hence it is illegal
            if (result == 1)
            {
                return this.Evaluate(false, context, notification);
            }

            var checkDigit = 0;
            if (result > 1)
            {
                checkDigit = 11 - result;
            }

            var isValid = checkDigit == idDigits[8];
            return this.Evaluate(isValid, context, notification);
        }
    }
}