using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebToolkit.Contracts.Factories
{
    public interface IRelationalMapperFactory
    {
        KeyValuePair<TMap, TKey> GetOrCreate<TEntity, TKey, TMap>(TMap mappedValue, Func<TMap, TKey> getKeyFunc);
        Task<KeyValuePair<TMap, TKey>> GetOrCreateAsync<TEntity, TKey, TMap>(TMap mappedValue, Func<TMap, Task<TKey>> getKeyFunc);
    }
}