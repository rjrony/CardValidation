using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using CardValidation.ApplicationService.MetaCardValidators;

namespace CardValidation.ApplicationService
{
    public class CardValidator : ICardValidator
    {
        public ValidateResultView CardValidate(CardValidationQueryRequest request)
        {
            //todo: static part
            var validateResult = MetaCardValidator.Validate(request.CardNumber, request.ExpiryDate);

            return validateResult;

            //todo: database check
        }
    }
}
