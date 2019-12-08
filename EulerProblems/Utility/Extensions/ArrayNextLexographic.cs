using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Utility.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Производит следующую лексикографическую перестановку в массиве
        /// </summary>
        /// <typeparam name="T">Тип поддерживающий IComparable</typeparam>
        /// <param name="arr">Массив в котором будет произведена перестановка</param>
        /// <returns>Была ли произведена перестановка</returns>
        public static bool NextLexographic<T>(this T[] arr) where T : IComparable
        {
            // начиная с конца находим элемент который требуется переставить, его позиция i - 1
            for (int i = arr.Length - 1; i > 0; i--)
            {
                if (arr[i - 1].CompareTo(arr[i]) < 0)
                {
                    // все элементы правее этого считаются суффиксом, находим в нем минмальный, но больше чем i - 1
                    int pivot = i;
                    for (int j = pivot; j < arr.Length; j++)
                    {
                        if(arr[j].CompareTo(arr[pivot]) < 0 && arr[i - 1].CompareTo(arr[j]) < 0)
                        {
                            pivot = j;
                        }
                    }
                    
                    // меняем местами выбранный элемент с подходящим элементов из суффикса
                    arr.Swap(i - 1, pivot);

                    // переворачиваем суффикс, чтобы получить корректную последовательность
                    for (int j = arr.Length - 1; j > i; j--)
                    {
                        arr.Swap(j, i);
                        i++;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
