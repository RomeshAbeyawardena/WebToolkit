using System.Collections.Generic;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public class DataPool<TModel, TKey> : IDataPool<TModel, TKey>
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly string _cacheKeyName = $"DataPool_{typeof(TModel).FullName}_{typeof(TKey).FullName}";
        private IDictionary<TKey, TModel> DataPoolDictionary
        {
            get => _cacheProvider.Get<IDictionary<TKey, TModel>>(CacheType.DistributedCache, _cacheKeyName).Result;
            set => _cacheProvider.Set(CacheType.DistributedCache, _cacheKeyName, value).Wait();
        }


        public DataPool(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public TModel Add(TKey key, TModel value)
        {
            var dataPoolDictionary = DataPoolDictionary 
                                     ?? new Dictionary<TKey, TModel>();

            if (dataPoolDictionary.ContainsKey(key))
                dataPoolDictionary.Remove(key);
            
            if (!dataPoolDictionary.TryAdd(key, value))
                return default;
            
            DataPoolDictionary = dataPoolDictionary;
            return value;
        }

        public TModel Retrieve(TKey key)
        {
            if (DataPoolDictionary == null)
                return default;

            return DataPoolDictionary.TryGetValue(key, out var value) ? value : default;
        }

        public IDictionary<TKey, TModel> ToDictionary()
        {
            return DataPoolDictionary;
        }
    }
}