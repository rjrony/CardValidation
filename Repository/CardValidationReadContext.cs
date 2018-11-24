using System.Data.Common;

namespace CardValidation.Repository
{
    public class CardValidationReadContext : CardValidationContext
    {
        public CardValidationReadContext()
        {
        }

        public CardValidationReadContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public CardValidationReadContext(DbConnection existingConnection)
            : base(existingConnection)
        {
            // TODO: Need to analyze the performance of this audit mechanism
            // this.Audit();
        }
    }
}