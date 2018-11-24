using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DynamicQuery;

namespace CardValidation.Api.Dtos.Request
{
    public class CardValidationQueryRequest /*: DynamicQueryRequest*/
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
    }
}
