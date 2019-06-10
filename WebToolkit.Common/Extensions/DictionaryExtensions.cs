using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

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

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(a => new KeyValuePair<TKey, TValue>(a.Key, a.Value)).ToArray();
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            params KeyValuePair<TKey, TValue>[] keyValuePairs)
        {
            dictionary.AddRange(keyValuePairs.AsEnumerable());
        }

        public static JObject AsJObject<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, string> keyToString = null)
        {
            if (keyToString == null)
                keyToString = key => key.ToString();

            var jObject = new JObject();

            foreach (var keyValuePair in dictionary)
            {
                jObject.Add(keyToString.Invoke(keyValuePair.Key), 
                    JToken.FromObject(keyValuePair.Value));
            }

            return jObject;
        }
    }
}