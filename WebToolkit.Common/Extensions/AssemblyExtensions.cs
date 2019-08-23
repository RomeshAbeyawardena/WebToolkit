using System.Reflection;

namespace WebToolkit.Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static Assembly GetAssembly<T>(this Assembly assembly)
        {
            return Assembly.GetAssembly(typeof(T));
        }
    }
}