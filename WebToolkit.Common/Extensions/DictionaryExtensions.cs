using System.Collections.Generic;
using System.Linq;

namespace WebToolkit.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(a => new KeyValuePair<TKey, TValue>(a.Key, a.Value));
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            params KeyValuePair<TKey, TValue>[] keyValuePairs)
        {
            dictionary.AddRange(keyValuePairs.AsEnumerable());
        }
    }
}