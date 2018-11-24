using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using CardValidation.Repository.Models.Enums;

namespace CardValidation.ApplicationService.MetaCardValidators
{
    public class JCBValidator : IMetaCardValidator
    {
        public ValidateResultView Validate(string cardNumber, short year)
        {
            var validateResult = new ValidateResultView
            {
                CardType = CardTypeEnum.JCB.ToString("G")
            };

            if (cardNumber.Length == 16)
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