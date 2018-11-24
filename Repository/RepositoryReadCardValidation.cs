using Infrastructure.ReadModel;
using Infrastructure.Repository.Contracts;

namespace CardValidation.Repository
{
    public class RepositoryReadCardValidation : ReadOptimizedRepository<CardValidationReadContext>, IRepositoryReadCardValidation
    {
        public RepositoryReadCardValidation(IDbContextFactory<CardValidationReadContext> dbContextProvider): base(dbContextProvider)
        {
            
        }
    }
}
