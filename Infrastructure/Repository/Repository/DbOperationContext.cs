using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    class DbOperationContext: IOperationContext
    {
        public string CorrelationId { get; set; }
    }
}
