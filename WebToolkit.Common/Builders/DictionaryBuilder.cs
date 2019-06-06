using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts.Builders;

namespace WebToolkit.Common.Builders
{
    public sealed class DictionaryBuilder<TKey, TValue> : IDictionaryBuilder<TKey, TValue>
    {
        public TValue this[TKey key] => _internalDictionary[key];

        public static IDictionaryBuilder<TKey, TValue> Create(IDictionary<TKey, TValue> dictionary = null)
        {
            return new DictionaryBuilder<TKey, TValue>(dictionary ?? new Dictionary<TKey, TValue>());
        }

        public static IDictionaryBuilder<TKey, TValue> Create(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            return new DictionaryBuilder<TKey, TValue>(keyValuePairs);
        }

        public IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            _internalDictionary.Add(key, value);
            return this;
        }

        public IDictionaryBuilder<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            _internalDictionary.AddRange(keyValuePairs);

            return this;
        }

        public bool ContainsKey(TKey key)
        {
            return _internalDictionary.ContainsKey(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return _internalDictionary.Contains(keyValuePair);
        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            return _internalDictionary;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs()
        {
            return _internalDictionary.ToKeyValuePair();
        }

        
        private DictionaryBuilder(IDictionary<TKey, TValue> dictionary)
        {
            _internalDictionary = dictionary ?? new Dictionary<TKey, TValue>();
        }

        private DictionaryBuilder(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            _internalDictionary = new Dictionary<TKey, TValue>();
            AddRange(keyValuePairs);
        }

        private IEnumerator<KeyValuePair<TKey,TValue>> Enumerator => _internalDictionary.GetEnumerator();
        private readonly IDictionary<TKey, TValue> _internalDictionary;
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}