using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface ICachedDictionary<TKey, TValue>
    {
        IDictionary<TKey, TValue> Dictionary { get; set; }
        bool TryGetValue(TKey key, out TValue value);
        void AddOrReplace(TKey key, TValue value);
        void Remove(params TKey[] keys);
    }
}