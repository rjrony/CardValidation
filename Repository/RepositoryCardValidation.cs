using Infrastructure.ReadModel;
using Infrastructure.Repository.Contracts;

namespace CardValidation.Repository
{
    public class RepositoryCardValidation : Repository<CardValidationContext>, IRepositoryCardValidation
    {
        public RepositoryCardValidation(IDbContextFactory<CardValidationContext> dbContextProvider): base(dbContextProvider)
        {
            
        }
    }
}
