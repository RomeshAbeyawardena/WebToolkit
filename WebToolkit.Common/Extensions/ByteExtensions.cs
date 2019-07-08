using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebToolkit.Common.Extensions
{
    public static class ByteExtensions
    {
        public static string ToString(this IEnumerable<byte> value, Encoding encoding)
        {
            return encoding.GetString(value.ToArray());
        }
    }
}