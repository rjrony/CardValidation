using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardValidation.ApplicationService
{
    public static class UtilityService
    {
        public static bool IsLeapYear(short year)
        {
            if ((year % 400 == 0 || year % 100 != 0 && year % 4 == 0))
            {
                return true;
            }

            return false;
        }

        public static bool IsPrime(this short number)
        {
            if ((number % 2) == 0)
            {
                return number == 2;
            }
            int sqrt = (int)Math.Sqrt(number);
            for (int t = 3; t <= sqrt; t = t + 2)
            {
                if (number % t == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }
    }
}
