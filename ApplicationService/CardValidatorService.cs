using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Api.Dtos.Request;
using CardValidation.Api.Dtos.Views;
using CardValidation.ApplicationService.Contracts;
using CardValidation.ApplicationService.MetaCardValidators;
using CardValidation.Repository;
using CardValidation.Repository.Models;
using CardValidation.Repository.Models.Enums;
using Infrastructure.Interception.Contract;
using Infrastructure.Logging.Contracts;

namespace CardValidation.ApplicationService
{
    public class CardValidatorService : ICardValidatorService
    {
        private readonly IDependencyResolver dependencyResolver;
        private readonly ILogger logger;

        private readonly IRepositoryCardValidation repositoryCardValidation;
        public CardValidatorService(IDependencyResolver dependencyResolver, ILogger logger
        , IRepositoryCardValidation repositoryCardValidation)
        {
            this.dependencyResolver = dependencyResolver;
            this.logger = logger;
            this.repositoryCardValidation = repositoryCardValidation;
        }

        public async Task<ValidateResultView> CardValidate(CardValidationQueryRequest request)
        {
            //static part
            var validateResult = MetaCardValidator.Validate(request.CardNumber, request.ExpiryDate);
            this.logger.Debug(() => $"Validation result of card: {request.CardNumber} is: {validateResult.CardType}:{validateResult.Result}");

            if (validateResult.Result == ResultEnum.Valid.ToString("G"))
            {
                //database part
                var card = await this.repositoryCardValidation.Get<Card>(x => x.CardNumber == request.CardNumber)
                    .FirstOrDefaultAsync();

                if (card == null)
                {
                    validateResult.Result = ResultEnum.DoesNotExist.ToString("G");
                }
            }

            return validateResult;
        }
    }
}
