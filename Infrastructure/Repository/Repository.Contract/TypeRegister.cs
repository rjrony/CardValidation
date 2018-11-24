using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interception.Contract;

namespace Infrastructure.Repository.Contracts
{
    class TypeRegister : ITypeRegistrar
    {
        public void Register(ITypeRegistrarService typeRegistrarService)
        {
            typeRegistrarService.RegisterTypeSingleton<IDbInitializerConfig, DefaultDbInitializerConfig>();
        }
    }
}
