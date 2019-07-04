using System.Collections.Generic;

namespace WebToolkit.Contracts.Builders
{
    public interface IDictionaryBuilder<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        TValue this[TKey key] { get; }
        IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
        IDictionaryBuilder<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs);
        bool ContainsKey(TKey key);
        bool Contains(KeyValuePair<TKey, TValue> keyValuePair);
        IDictionary<TKey, TValue> ToDictionary();
        IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs();
    }
}