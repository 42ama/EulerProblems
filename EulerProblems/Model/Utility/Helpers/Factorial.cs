using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        public static BigInteger Factorial(long number)
        {
            BigInteger value = new BigInteger(1);
            if(number < 0)
            {
                throw new ArgumentOutOfRangeException("Число должно быть больше 0");
            }
            else if(number < 2)
            {
                return value;
            }
            else
            {
                for (long i = 2; i <= number; i++)
                {
                    value *= i;
                }
                return value;
            }
        }
    }
}
