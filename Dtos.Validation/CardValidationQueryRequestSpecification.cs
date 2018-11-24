using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Api.Dtos.Request;
using SpecExpress;

namespace CardValidation.Api.Dtos.Validation
{
    public class CardValidationQueryRequestSpecification : Validates<CardValidationQueryRequest>
    {
        public CardValidationQueryRequestSpecification()
        {
            this.Check(x => x.CardNumber).Required().LengthBetween(15, 16);
            this.Check(x => x.ExpiryDate).Required().LengthEqualTo(6);
        }
    }
}
