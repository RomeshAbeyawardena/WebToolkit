using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebToolkit.Contracts
{
    public interface IRelationalMapper<TEntity, TKey, TMap>
    {
        KeyValuePair<TMap, TKey> GetOrCreate(TMap mappedValue, Func<TMap, TKey> getKeyFunc);
        Task<KeyValuePair<TMap, TKey>> GetOrCreateAsync(TMap mappedValue, Func<TMap, Task<TKey>> getKeyFunc);
    }
}