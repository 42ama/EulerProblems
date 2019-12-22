using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        /// <summary>
        /// Возвращает наибольший общий делитель двух чисел
        /// </summary>
        public static int GreatestCommonDivisor(int a, int b)
        {
            int y;
            int x;

            if (a > b)
            {
                x = a;
                y = b;
            }
            else
            {
                x = b;
                y = a;
            }

            while (x % y != 0)
            {
                int temp = x;
                x = y;
                y = temp % x;
            }

            return y;
        }
    }
}
