using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using Infrastructure.Host;
using Infrastructure.Validation.WebApi;

namespace CardValidation.Api.Controllers
{
    public class CardController : ApiController
    {
        private readonly ICardValidatorService cardValidatorService;

        public CardController(ICardValidatorService cardValidatorService)
        {
            this.cardValidatorService = cardValidatorService;
        }
       
        [HttpGet]
        [Route(ApiPaths.ValidateCard)]
        public async Task<ValidateResultView> GetValidateCard(string cardNumber, string expiryDate)
        {
            CardValidationQueryRequest request = new CardValidationQueryRequest
            {
                CardNumber = cardNumber, ExpiryDate = expiryDate
            };

            var validateResult = await this.cardValidatorService.CardValidate(request);
            return validateResult;
        }

        [ValidateFilter]
        [HttpPost]
        [Route(ApiPaths.ValidateCard)]
        public async Task<ValidateResultView> PostValidateCard(CardValidationQueryRequest request)
        {
            var validateResult = await this.cardValidatorService.CardValidate(request);
            return validateResult;
        }

    }
}