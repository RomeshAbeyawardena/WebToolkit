using System.Collections.Concurrent;
using System.Collections.Generic;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    
    public sealed class CachedDictionary
    {
        public static ICachedDictionary<TKey, TValue> CreateCachedDictionary<TKey, TValue>(ICacheProvider cacheProvider,
            string cacheKey, CacheType cacheType, params KeyValuePair<TKey, TValue>[] dictionaryKeyValuePair)
        {
            return CachedDictionary<TKey, TValue>.CreateCachedDictionary(cacheProvider, cacheKey, cacheType,
                dictionaryKeyValuePair);
        }
    }

    public sealed class CachedDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly string _cacheKey;
        private readonly CacheType _cacheType;
        private readonly KeyValuePair<TKey, TValue>[] _dictionaryKeyValuePair;
        private IDictionary<TKey, TValue> CreateIfNull(bool saveToCache = false)
        {
            var dictionary = new ConcurrentDictionary<TKey, TValue>(_dictionaryKeyValuePair);
            if (saveToCache && Dictionary == null)
                Dictionary = dictionary;
            
            return Dictionary ?? dictionary;
        }

        public static ICachedDictionary<TKey, TValue> CreateCachedDictionary(ICacheProvider cacheProvider, string cacheKey, 
            CacheType cacheType, params KeyValuePair<TKey, TValue>[] dictionaryKeyValuePair)
        {
            return new CachedDictionary<TKey, TValue>(cacheProvider, cacheKey, cacheType, dictionaryKeyValuePair);
        }

        private CachedDictionary(ICacheProvider cacheProvider, string cacheKey, CacheType cacheType, params KeyValuePair<TKey, TValue>[] dictionaryKeyValuePair)
        {
            _cacheProvider = cacheProvider;
            _cacheKey = cacheKey;
            _cacheType = cacheType;
            _dictionaryKeyValuePair = dictionaryKeyValuePair;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var dictionary = CreateIfNull(true);
            return dictionary.TryGetValue(key, out value);
        }

        public void AddOrReplace(TKey key, TValue value)
        {
            var dictionary = CreateIfNull();

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else 
                dictionary.Add(key, value);

            Dictionary = dictionary;
        }

        public void Remove(params TKey[] keys)
        {
            var dictionary = CreateIfNull();

            foreach (var key in keys)
            {
                if(dictionary.ContainsKey(key))
                    dictionary.Remove(key);
            }

            Dictionary = dictionary;
        }

        public IDictionary<TKey, TValue> Dictionary
        {
            get => _cacheProvider
                .Get<IDictionary<TKey, TValue>>(_cacheType, _cacheKey)
                .Result;
            set => _cacheProvider
                .Set(_cacheType, _cacheKey, value)
                .Wait();
        }
    }
}