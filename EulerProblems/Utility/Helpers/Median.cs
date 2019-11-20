﻿using System;

namespace EulerProblems.Utility.Helpers
{
    public static partial class Helper
    {
        public static double Median(long[] arr)
        {
            double median;
            int length = arr.Length;
            Array.Sort(arr);

            if (length % 2 != 0)
            {
                median = arr[(length + 1) / 2 - 1];
            }
            else
            {
                median = (arr[length / 2 - 1] + arr[length / 2]) / 2.0d;
            }

            return median;
        }
    }
}
