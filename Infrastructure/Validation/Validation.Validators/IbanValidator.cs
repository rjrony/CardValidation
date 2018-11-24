// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IbanValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System.Globalization;
    using System.Text;

    using SpecExpress;
    using SpecExpress.Rules;

    /// <summary>
    /// The IBAN validator.
    ///     The IBAN can be of max length 34 and starts with ISO2 country code.
    ///     The following 2 digits are the control digits
    /// </summary>
    /// <typeparam name="T">
    /// string iban. Optional blanks are ignored
    /// </typeparam>
    public class IbanValidator<T> : RuleValidator<T, string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IbanValidator{T}" /> class.
        /// </summary>
        public IbanValidator()
        {
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.InvalidIbanNumber;
        }

        /// <summary>
        /// The validate.
        ///     1.Move the four initial characters to the end of the string
        ///     2.Replace the letters in the string with digits, expanding the string as necessary, such that A=10, B=11 and Z=35.
        ///     3.Convert the string to an integer and mod-97 the entire number
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
            if (context.PropertyValue == null)
            {
                return false;
            }

            var ibanValue = context.PropertyValue.Replace(" ", string.Empty);
            if (ibanValue.Length > 34)
            {
                return this.Evaluate(false, context, notification);
            }

            // CH & LI Validator
            var countryIso2 = ibanValue.Substring(0, 2);
            if (countryIso2 == "CH" || countryIso2 == "LI")
            {
                if (ibanValue.Length != 21)
                {
                    return this.Evaluate(false, context, notification);
                }
            }

            // General IBAN Validator
            if (System.Text.RegularExpressions.Regex.IsMatch(ibanValue, "^[A-Z0-9]"))
            {
                ibanValue = ibanValue.Replace(" ", string.Empty);
                var iban = ibanValue.Substring(4, ibanValue.Length - 4) + ibanValue.Substring(0, 4);
                const int AsciiShift = 55;
                var sb = new StringBuilder();
                foreach (var c in iban)
                {
                    int v;
                    if (char.IsLetter(c))
                    {
                        v = c - AsciiShift;
                    }
                    else
                    {
                        v = int.Parse(c.ToString(CultureInfo.InvariantCulture));
                    }

                    sb.Append(v);
                }

                var checkSumString = sb.ToString();
                var checksum = int.Parse(checkSumString.Substring(0, 1));
                for (var i = 1; i < checkSumString.Length; i++)
                {
                    var v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }

                var isValid = checksum == 1;
                return this.Evaluate(isValid, context, notification);
            }

            return this.Evaluate(false, context, notification);
        }
    }
}