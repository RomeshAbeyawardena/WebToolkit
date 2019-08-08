using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebToolkit.Common.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<byte> ToByteEnumerable(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static string ReplaceAll(this string value, string replacement, params string[] replaceValues)
        {
            return replaceValues.Aggregate(value, (current, replaceValue) => current.Replace(replaceValue, replacement));
        }
    }
}