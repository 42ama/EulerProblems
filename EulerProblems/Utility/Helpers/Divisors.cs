using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProject.Utility.Helpers
{
    public static partial class Helper
    {
        public static int DivisorsCount(long number)
        {
            return Divisors(number).Count;
        }

        public static ISet<long> Divisors(long number)
        {
            // первый общий делитель - единица
            HashSet<long> set = new HashSet<long> { 1 };
            for (long i = 2; i <= (long)Math.Floor(Math.Sqrt(number)); i++)
            {
                if (number % i == 0)
                {
                    set.Add(i);
                    if (number / i != i)
                    {
                        set.Add(number / i);
                    }
                }
            }
            // второй общий делитель - само число
            set.Add(number);
            return set;
        }
    }
}
