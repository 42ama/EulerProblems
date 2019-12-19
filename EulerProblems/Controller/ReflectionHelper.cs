using System;
using System.Linq;
using System.Reflection;

namespace EulerProblems.Controller
{
    public static class ReflectionHelper
    {
        public static Type[] GetTypesInNamespace(string nameSpace)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return GetTypesInNamespace(nameSpace,assembly);
        }

        public static Type[] GetTypesInNamespaceStartsWith(string startsWith)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return GetTypesInNamespaceStartsWith(startsWith, assembly);
        }

        public static Type[] GetTypesInNamespace(string nameSpace, Assembly assembly)
        {
            return assembly.GetTypes()
                      .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        public static Type[] GetTypesInNamespaceStartsWith(string startsWith, Assembly assembly)
        {
            return assembly.GetTypes()
                      .Where(t => t.Namespace?.StartsWith(startsWith) ?? false)
                      .ToArray();
        }
    }
}
