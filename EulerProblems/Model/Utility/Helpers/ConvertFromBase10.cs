using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        private static char[] lettersForBases = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// Преобразует число из 10ой системы счисления в число в заданной системе счисления
        /// </summary>
        /// <param name="number">Число которое будет преобразовано</param>
        /// <param name="toBase">Система счисления конечного числа. В пределах от 2 до 35.</param>
        public static char[] ConvertFromBase10(int number, int toBase)
        {
            if(toBase < 2 || toBase > 35)
            {
                throw new ArgumentOutOfRangeException("toBase", "Система счисления должна находится в пределах от 2 до 35.");
            }

            if(toBase<=10)
            {
                return ConvertFromBase10Lesser(number, toBase);
            }
            else
            {
                return ConvertFromBase10Greater(number, toBase);
            }
        }

        private static char[] ConvertFromBase10Lesser(int number, int toBase)
        {
            var result = new StringBuilder();

            while (number >= toBase)
            {
                var mod = number % toBase;
                number /= toBase;
                result.Append(mod);
            }

            result.Append(number);

            return result.ToString().Reverse().ToArray();
        }

        private static char[] ConvertFromBase10Greater(int number, int toBase)
        {
            var result = new StringBuilder();

            while (number >= toBase)
            {
                var mod = number % toBase;
                number /= toBase;
                // на самом деле mod может быть больше 9, только когда toBase > 10, а значит код данного метода
                // может быть использован для всех случаев toBase = 2..35, однако разделение на два метода
                // более конкретно показывает различный подход
                if (mod > 9)
                {
                    result.Append(lettersForBases[mod - 10]);
                }
                else
                {
                    result.Append(mod);
                }
            }

            result.Append(number);

            return result.ToString().Reverse().ToArray();
        }
    }
}
