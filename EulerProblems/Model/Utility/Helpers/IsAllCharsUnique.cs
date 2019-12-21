using System;
using System.Collections.Generic;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
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
