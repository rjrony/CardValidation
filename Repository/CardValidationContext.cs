using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CardValidation.Repository.EtConfiguration;
using CardValidation.Repository.Models;
using Infrastructure.Repository;

namespace CardValidation.Repository
{
    public class CardValidationContext : ContextBase
    {
        public CardValidationContext()
        {
            
        }

        public CardValidationContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            
        }

        public CardValidationContext(DbConnection existingConnection)
            : base(existingConnection)
        {
            // TODO: Need to analyze the performance of this audit mechanism
           // this.Audit();
        }


        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new CardConfiguration());

            base.OnModelCreating(modelBuilder);
        }


    }
}