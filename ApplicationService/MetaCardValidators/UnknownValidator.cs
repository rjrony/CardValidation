using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;

namespace CardValidation.ApplicationService.MetaCardValidators
{
    public class UnknownValidator : IMetaCardValidator
    {
        public ValidateResultView Validate(string cardNumber, short year)
        {
            var validateResult = new ValidateResultView();
            return validateResult;
        }
    }
}