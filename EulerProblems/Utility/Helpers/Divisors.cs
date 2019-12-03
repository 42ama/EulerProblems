using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Utility.Helpers
{
    public static partial class Helper
    {
        public static int DivisorsCount(long number)
        {
            return Divisors(number).Count;
        }

        /// <summary>
        /// Возвращает сэт делителей для числа, включая единицу и само число
        /// </summary>
        /// <param name="number">Число для которого считаются делители</param>
        /// <returns>Сэт делителей для числа, включая единицу и само число</returns>
        public static ISet<long> Divisors(long number)
        {
            ISet<long> set = DivisorsWithoutTypical(number);

            // первый общий делитель - единица
            set.Add(1);
            // второй общий делитель - само число
            set.Add(number);
            return set;
        }

        /// <summary>
        /// Возвращает сэт уникальных делителей для числа, то есть тот в который не включается само число и единица
        /// </summary>
        /// <param name="number">Число для которого считаются делители</param>
        /// <returns>Сэт уникальных делителей для числа</returns>
        public static ISet<long> DivisorsWithoutTypical(long number)
        {
            HashSet<long> set = new HashSet<long>();
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
            return set;
        }
    }
}
