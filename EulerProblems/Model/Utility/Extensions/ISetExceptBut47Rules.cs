using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Удаляет из оригинального множества элементы найденные в сравниваемом множестве, однако
        /// добавляет первую не найденную в сравниваемом множестве степень найденного элемента
        /// </summary>
        /// <param name="set">Оригинальное множество, из которого будут удалены найденные элементы</param>
        /// <param name="collection">Множество с которым происходит сравнение</param>
        /// <param name="number">Число порадивашее оригинальное множество</param>
        public static void ExceptBut47Rules(this ISet<int> set, ISet<int> collection, int number)
        {
            foreach (var item in collection)
            {
                if (set.Contains(item))
                {
                    set.Remove(item);
                    int rank = 2;
                    int power = (int)Math.Pow(item, rank);
                    while (power <= number)
                    {
                        if(collection.Contains(power))
                        {
                            rank++;
                            power = (int)Math.Pow(item, rank);
                        }
                        else
                        {
                            set.Add(power);
                            break;
                        }
                    }
                }

            }
        }
    }
}
