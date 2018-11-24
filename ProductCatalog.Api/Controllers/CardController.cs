using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using Infrastructure.Validation.WebApi;

namespace CardValidation.Api.Controllers
{
    public class CardController : ApiController
    {
        private readonly ICardValidator cardValidator;

        public CardController(ICardValidator cardValidator)
        {
            this.cardValidator = cardValidator;
        }
       
        [HttpGet]
        [Route(ApiPaths.ValidateCard)]
        public async Task<ValidateResultView> GetValidateCard(string cardNumber, string expiryDate)
        {
            CardValidationQueryRequest request=new CardValidationQueryRequest();
            request.CardNumber = cardNumber;
            request.ExpiryDate = expiryDate;

            var validateResult = this.cardValidator.CardValidate(request);
            await Task.Delay(10);
            return validateResult;
        }

        [ValidateFilter]
        [HttpPost]
        [Route(ApiPaths.ValidateCard)]
        public async Task<ValidateResultView> PostValidateCard(CardValidationQueryRequest request)
        {
            var validateResult = this.cardValidator.CardValidate(request);
            await Task.Delay(10);
            return validateResult;
        }

    }
}