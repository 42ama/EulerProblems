using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Extensions
{
    public static partial class Extension
    {
        public static int CountDigits(this int number)
        {
            return (int)Math.Floor(Math.Log10(number) + 1);
        }
    }
}
