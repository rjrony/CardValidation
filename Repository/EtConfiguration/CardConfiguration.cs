using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardValidation.Repository.Models;

namespace CardValidation.Repository.EtConfiguration
{
    public class CardConfiguration : EntityTypeConfiguration<Card>
    {
        public CardConfiguration()
        {
            this.Property(x => x.CardNumber).IsRequired().HasMaxLength(16);
            this.Property(x => x.ExpiryDate).IsRequired().HasMaxLength(6);
        }
    }
}
