using System;

namespace EulerProject.Utility.Helpers
{
    public static partial class Helper
    {
        public static bool IsPalindome(char[] numeric)
        {
            for (int i = 0; i < (int)Math.Ceiling((double)numeric.Length / 2); i++)
            {
                if (numeric[i] != numeric[numeric.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
