using System;
using System.Threading.Tasks;

namespace WebToolkit.Contracts.Factories
{
    public interface IDataPoolFactory
    {
        TEntity GetValue<TEntity, TKey>(TKey key, Func<TKey, TEntity> valueFunc);
        Task<TEntity> GetValueAsync<TEntity, TKey>(TKey key, Func<TKey, Task<TEntity>> valueFunc);
    }
}