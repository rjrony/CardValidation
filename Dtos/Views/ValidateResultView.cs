using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Repository.Models.Enums;

namespace CardValidation.Api.Dtos.Views
{
    public class ValidateResultView
    {
        public ValidateResultView()
        {
            this.CardType = CardTypeEnum.Unknown.ToString("G");
            //this.Result = ResultEnum.Invalid.ToString("G");
        }
        public string Result { get; set; }
        public string CardType { get; set; }
    }
}
