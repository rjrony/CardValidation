using System;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;

namespace CardValidation.ApplicationService.MetaCardValidators
{
    public static class MetaCardValidator
    {
        public static ValidateResultView Validate(string cardNumber, string expiryDate)
        {
            var year = Convert.ToInt16(expiryDate.Substring(2));

            IMetaCardValidator validator = SelectCardValidator(cardNumber);

            return validator.Validate(cardNumber, year);
        }

        private static IMetaCardValidator SelectCardValidator(string cardNumber)
        {
            var firstFourDigit = Convert.ToInt16(cardNumber.Substring(0, 4));
            if (cardNumber.StartsWith("4"))//Visa
            {
                return new VisaValidator();
            }
            else if (cardNumber.StartsWith("5"))//Master
            {
                return new MasterValidator();
            }
            else if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))//Amex
            {
                return new AmexValidator();
            }
            else if (firstFourDigit >= 3528 && firstFourDigit <= 3589)//JCB
            {
                return new JCBValidator();
            }
            else//Unknown
            {
                return new UnknownValidator();
            }
        }
    }
}
