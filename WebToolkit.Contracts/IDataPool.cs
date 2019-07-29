using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface IDataPool<TModel, TKey>
    {
        TModel Add(TKey key, TModel value);
        TModel Retrieve(TKey key);
        IDictionary<TKey, TModel> ToDictionary();
    }
}