using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Меняет местами два элемента в массиве
        /// </summary>
        /// <param name="arr">Данный массив</param>
        /// <param name="indexA">Индекс первого элемента</param>
        /// <param name="indexB">Индекс второго элемента</param>
        public static void Swap<T>(this T[] arr, int indexA, int indexB)
        {
            T tempValueA = arr[indexA];
            arr[indexA] = arr[indexB];
            arr[indexB] = tempValueA;
        }

        public static void Swap<T>(this T[] arr, Index indexA, Index indexB)
        {
            T tempValueA = arr[indexA];
            arr[indexA] = arr[indexB];
            arr[indexB] = tempValueA;
        }
    }
}
