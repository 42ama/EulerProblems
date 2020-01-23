using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        public static int DivisorsCount(long number)
        {
            return Divisors(number).Count;
        }

        /// <summary>
        /// Возвращает множество делителей для числа, включая единицу и само число
        /// </summary>
        /// <param name="number">Число для которого считаются делители</param>
        /// <returns>Множество делителей для числа, включая единицу и само число</returns>
        public static ISet<long> Divisors(long number)
        {
            ISet<long> set = DivisorsWithoutTypical(number);

            // первый общий делитель - единица
            set.Add(1);
            // второй общий делитель - само число
            set.Add(number);
            return set;
        }
        public static ISet<int> Divisors(int number)
        {
            ISet<int> set = DivisorsWithoutTypical(number);

            // первый общий делитель - единица
            set.Add(1);
            // второй общий делитель - само число
            set.Add(number);
            return set;
        }


        /// <summary>
        /// Возвращает множество уникальных делителей для числа, то есть то в которое не включается само число и единица
        /// </summary>
        /// <param name="number">Число для которого считаются делители</param>
        /// <returns>Множество уникальных делителей для числа</returns>
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
        public static ISet<int> DivisorsWithoutTypical(int number)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 2; i <= (int)Math.Floor(Math.Sqrt(number)); i++)
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

        /// <summary>
        /// Возвращает множество уникальных простых делителей для числа, то есть то в которое не включается само число и единица 
        /// </summary>
        /// <param name="number">Число для которого считаются делители</param>
        /// <returns>Множество уникальных простых делителей для числа</returns>
        public static ISet<int> DivisorsPrimeWithoutTypical(int number)
        {
            var divisors = DivisorsWithoutTypical(number);
            var primeDivisors = new HashSet<int>();
            foreach (var divisor in divisors)
            {
                if (Helper.IsPrime(divisor))
                {
                    primeDivisors.Add(divisor);
                }
            }

            return primeDivisors;
        }
    }
}
