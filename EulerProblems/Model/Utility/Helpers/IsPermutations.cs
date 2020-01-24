using System;
using System.Collections.Generic;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        /// <summary>
        /// Проверяет являются ли все строки в исходном массиве - перестановками друг друга
        /// </summary>
        /// <param name="lines">Исходный массив</param>
        /// <returns>true, если все строки перестановки друг друга и false в противном случае</returns>
        public static bool IsPermutations(char[][] lines)
        {
            // с первой строки из массива собираем словарь, относительно которого будем сравнивать другие строки
            var compareDict = new Dictionary<char, int>();

            foreach (var symbol in lines[0])
            {
                if (compareDict.ContainsKey(symbol))
                {
                    compareDict[symbol]++;
                }
                else
                {
                    compareDict[symbol] = 1;
                }
            }

            // для каждой строки соберем словарь используемых символов
            // и сравним с словарем предназначеным для сравнения
            foreach (var line in lines[1..])
            {
                var dict = new Dictionary<char, int>();
                foreach (var symbol in line)
                {
                    if (dict.ContainsKey(symbol))
                    {
                        dict[symbol]++;
                    }
                    else
                    {
                        dict[symbol] = 1;
                    }
                }

                // итерационный перебор обоих коллекций даёт выигрыш
                // в скорости около 2х раз по сравнению с использованием
                // связки Linq.Except() и Linq.Count() 

                // сравним обе коллекции между собой
                foreach (var kvp in dict)
                {
                    if (!compareDict.ContainsKey(kvp.Key))
                    {
                        return false;
                    }
                    else
                    {
                        if (compareDict[kvp.Key] != kvp.Value)
                        {
                            return false;
                        }
                    }
                }

                foreach (var kvp in compareDict)
                {
                    if (!dict.ContainsKey(kvp.Key))
                    {
                        return false;
                    }
                    else
                    {
                        if (dict[kvp.Key] != kvp.Value)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
