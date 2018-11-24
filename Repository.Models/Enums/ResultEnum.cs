using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardValidation.Repository.Models.Enums
{
    public enum ResultEnum
    {
        Valid = 1,
        Invalid = 2,
        //[StringValue("Does not exist")]
        DoesNotExist = 3,
    }
}
