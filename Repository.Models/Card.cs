using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Repository.Models.Enums;

namespace CardValidation.Repository.Models
{
    public class Card
    {
        public long CardId { get; set; }
        public string CardNumber { get; set; }
        public CardTypeEnum CardType { get; set; }
        public string ExpiryDate { get; set; }
    }
}
