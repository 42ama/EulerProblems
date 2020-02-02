using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace EulerProblems.Model.Utility.Extensions
{
    public static partial class Extension
    {
        public static int Length(this int number)
        {
            if (number < 0)
            {
                number = 0 - number;
            }
            if (number == 0)
            {
                return 1;
            }
                
            return (int)Math.Floor(Math.Log10(number)) + 1;
        }

        public static int Length(this long number)
        {
            if (number < 0)
            {
                number = 0 - number;
            }
            if (number == 0)
            {
                return 1;
            }

            return (int)Math.Floor(Math.Log10(number)) + 1;
        }

        public static int Length(this BigInteger number)
        {
            if (number < 0)
            {
                number = 0 - number;
            }
            if (number == 0)
            {
                return 1;
            }

            return (int)Math.Floor(BigInteger.Log10(number)) + 1;
        }
    }
}
