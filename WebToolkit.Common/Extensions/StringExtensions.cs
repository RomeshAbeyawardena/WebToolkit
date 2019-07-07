using System.Collections.Generic;
using System.Text;

namespace WebToolkit.Common.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<byte> ToByteEnumerable(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }
    }
}