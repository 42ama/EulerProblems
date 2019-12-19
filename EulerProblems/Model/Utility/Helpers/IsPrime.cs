using System;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        public static bool IsPrime(int number)
        {
            if (number <= 1) { return false; }
            if (number == 2) { return true; }
            if (number % 2 == 0) { return false; }

            int boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) { return false; }                    
            }

            return true;
        }

        public static bool IsPrime(long number)
        {
            if (number <= 1) { return false; }
            if (number == 2) { return true; }
            if (number % 2 == 0) { return false; }

            long boundary = (long)Math.Floor(Math.Sqrt(number));

            for (long i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) { return false; }
            }

            return true;
        }
    }
}
