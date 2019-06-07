using WebToolkit.Contracts;

namespace WebToolkit.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object ValueOrDefault(this object value, object defaultValue)
        {
            return (value is string strValue) 
                ? ValueOrDefault(strValue, defaultValue as string) 
                : value ?? defaultValue;
        }

        public static string ValueOrDefault(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? value : defaultValue;
        }

        public static byte[] GetBytes(this string value, IEncodingProvider encodingProvider, Contracts.Encoding encoding)
        {
            return encodingProvider.GetBytes(value, encoding);
        }

        public static string GetString(this byte[] byteValue, IEncodingProvider encodingProvider, Contracts.Encoding encoding)
        {
            return encodingProvider.GetString(byteValue, encoding);
        }
    }
}