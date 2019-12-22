using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        /// <summary>
        /// Возвращает список цифр из числа
        /// </summary>
        public static IEnumerable<int> GetAllDigits(int number)
        {
            var digits = new List<int>();

            while(number > 0)
            {
                digits.Add(number % 10);
                number /= 10;
            }

            digits.Reverse();

            return digits;
        }

        /// <summary>
        /// Возвращает список цифр из числа
        /// </summary>
        public static IEnumerable<int> GetAllDigits(long number)
        {
            var digits = new List<int>();

            while (number > 0)
            {
                digits.Add((int)(number % 10));
                number /= 10;
            }

            digits.Reverse();

            return digits;
        }
    }
}
