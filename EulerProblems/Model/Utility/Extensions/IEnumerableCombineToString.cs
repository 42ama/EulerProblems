using System;
using System.Collections.Generic;
using System.Text;

namespace EulerProblems.Model.Utility.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Собирает коллекцию в сторку используя ToString()
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <returns>Строка из элементов коллекции</returns>
        public static string CombineToString<T>(this IEnumerable<T> collection)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }

    }
}
