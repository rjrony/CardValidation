using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CardValidation.Repository.Models;
using CardValidation.Repository.Models.Enums;

namespace CardValidation.Repository
{
    public class DefaultData
    {
        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Seed(CardValidationContext context)
        {
            this.GetCards().ForEach(c => context.Cards.AddOrUpdate(c));

            context.SaveChanges();
        }

        public List<Card> GetCards()
        {
            var list = new List<Card>
            {
                new Card
                {
                    CardType = CardTypeEnum.Visa,
                    CardNumber = "4412345678901234",
                    ExpiryDate = "012020"
                },
                new Card
                {
                    CardType = CardTypeEnum.Visa,
                    CardNumber = "4512345678901234",
                    ExpiryDate = "012024"
                },
                new Card
                {
                    CardType = CardTypeEnum.Master,
                    CardNumber = "5512345678901234",
                    ExpiryDate = "012021"
                }
            };

            return list;
        }
    }
}
