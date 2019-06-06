using System.Collections.Generic;
using System.Text;

namespace WebToolkit.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object ValueOrDefault(this object value, object defaultValue)
        {
            return value ?? defaultValue;
        }

        public static string ValueOrDefault(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? value : defaultValue;
        }

        public static byte[] GetByteArray(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static string GetString(this byte[] byteValue, Encoding encoding)
        {
            return encoding.GetString(byteValue);
        }
    }
}