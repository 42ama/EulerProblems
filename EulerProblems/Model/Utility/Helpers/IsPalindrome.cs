﻿using System;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        /// <summary>
        /// Возвращает true если число является палиндромом(зеркально относительно середины) и false в противном случае
        /// </summary>
        public static bool IsPalindrome(char[] numeric)
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
