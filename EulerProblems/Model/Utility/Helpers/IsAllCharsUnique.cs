using System;
using System.Collections.Generic;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        /// <summary>
        /// Возвращает true если все символы линии уникальные и false в противном случае
        /// </summary>
        public static bool IsAllCharsUnique(string line)
        {
            var set = new HashSet<char>();
            foreach (var chara in line)
            {
                if (set.Contains(chara))
                {
                    return false;
                }
                set.Add(chara);
            }
            return true;
        }
    }
}
