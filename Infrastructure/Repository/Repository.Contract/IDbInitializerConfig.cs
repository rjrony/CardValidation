using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Contracts
{
   public interface IDbInitializerConfig
    {
        bool ExceptionOnMigrationMissmatch { get; set; }
    }
}
