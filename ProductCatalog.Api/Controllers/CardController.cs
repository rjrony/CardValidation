using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;

namespace CardValidation.Api.Controllers
{
    public class CardController : ApiController
    {
        [HttpGet]
        [Route(ApiPaths.ValidateCard)]
        public async Task<ValidateResultView> GetValidateCard(CardValidationQueryRequest request)
        {

            await Task.Delay(10);
            return new ValidateResultView(); ;
        }


    }
}