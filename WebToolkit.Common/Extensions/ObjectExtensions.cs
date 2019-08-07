using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebToolkit.Contracts.Providers;

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

        public static byte[] GetBytes(this string value, IEncodingProvider encodingProvider, Encoding encoding)
        {
            return encodingProvider.GetBytes(value, encoding);
        }

        public static string GetString(this byte[] byteValue, IEncodingProvider encodingProvider, Encoding encoding)
        {
            return encodingProvider.GetString(byteValue, encoding);
        }

        public static async Task ForEach<T>(this IEnumerable<T> items, Func<T, Task> action)
        {
            foreach (var item in items)
            {
                await action(item);   
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);   
            }
        }

        public static IEnumerable<object> GetKeyProperties(this object value)
        {
            var valueType = value.GetType();

            return valueType.GetProperties()
                .Where(a => a.TryGetCustomAttribute<KeyAttribute>(out var attr))
                .Select(pi => pi.GetValue(value)).ToArray();
        }

        public static TKeySelector Apply<T, TKeySelector>(this T target, Action<PropertyInfo, object> applyAction, Func<T, TKeySelector> keySelector)
        {
            var oldValue = keySelector(target);
            typeof(T).Apply(applyAction, target, oldValue.GetType().Name);
            return oldValue;
        }

        public static bool IsDefault(this object val)
        {
            switch (val)
            {
                case byte byteVal:
                    return byteVal == default;
                case Guid gUid:
                    return gUid == default;
                case short shortVal :
                    return shortVal == default;
                case int intVal:
                    return intVal == default;
                case long longVal:
                    return longVal == default;
                case decimal decimalVal:
                    return decimalVal == default;
                case float floatVal:
                    return floatVal == default;
                case string stringVal:
                    return string.IsNullOrEmpty(stringVal);
                default:
                    return false;
            }
        }
    }
}