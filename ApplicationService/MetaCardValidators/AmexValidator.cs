using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using CardValidation.Repository.Models.Enums;

namespace CardValidation.ApplicationService.MetaCardValidators
{
    public class AmexValidator : IMetaCardValidator
    {
        public ValidateResultView Validate(string cardNumber, short year)
        {
            var validateResult = new ValidateResultView
            {
                CardType = CardTypeEnum.Amex.ToString("G")
            };

            if (cardNumber.Length == 15)
            {
                validateResult.Result = ResultEnum.Valid.ToString("G");
            }
            else
            {
                validateResult.Result = ResultEnum.Invalid.ToString("G");
            }

            return validateResult;
        }
    }
}