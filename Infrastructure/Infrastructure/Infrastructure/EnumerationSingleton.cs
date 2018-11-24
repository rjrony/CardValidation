using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class EnumerationSingleton<T, T2> : Enumeration<T> where T : IComparable<T>, new()
                                                                                      where T2 : EnumerationSingleton<T, T2>, new()
    {
        private static T2 instance;
        protected EnumerationSingleton(T t) : base(t)
        {

        }
        public static T2 GetInstance()
        {
            return instance ?? (instance = new T2());
        }
    }
}
