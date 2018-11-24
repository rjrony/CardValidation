using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Api.Dtos.Views;

namespace CardValidation.ApplicationService.Contracts
{
    public interface IMetaCardValidator
    {
        ValidateResultView Validate(string cardNumber, short year);
    }
}
