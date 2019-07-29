using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public class RelationalMapper<TEntity, TKey, TMap> : IRelationalMapper<TEntity, TKey, TMap>
    {
        private readonly string _cacheName = $"RelationalMappings({typeof(TEntity).Name}_{typeof(TKey).Name}_{typeof(TMap).Name})";
        
        private readonly ILogger<RelationalMapper<TEntity, TKey, TMap>> _logger;
        private readonly ICacheProvider _cacheProvider;

        public RelationalMapper(ILogger<RelationalMapper<TEntity, TKey, TMap>> logger, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
            _logger.LogInformation($"Initializing {typeof(IRelationalMapper<TEntity, TKey, TMap>).FullName}");
        }

        public KeyValuePair<TMap, TKey> GetOrCreate(TMap mappedValue, Func<TMap, TKey> getKeyFunc)
        {
            var relationalMappings = RelationalMappings ?? new Dictionary<TMap, TKey>();

            if (TryGetKeyValuePair(relationalMappings, mappedValue, out var keyValuePair))
            {
                _logger.LogInformation("Found mapping for {0}. Value is {1}", mappedValue, keyValuePair.Value);
                return keyValuePair;
            }

            var key = getKeyFunc(mappedValue);

            var addedKeyValuePair = AddMapping(relationalMappings, mappedValue, key);

            if (!addedKeyValuePair.HasValue)
                return default;

            RelationalMappings = relationalMappings;

            _logger.LogInformation("Cached new mapping for {0}. Value is {1}", mappedValue, key);

            return keyValuePair;
        }

        public async Task<KeyValuePair<TMap, TKey>> GetOrCreateAsync(TMap mappedValue, Func<TMap, Task<TKey>> getKeyFunc)
        {
            var relationalMappings = RelationalMappings ?? new Dictionary<TMap, TKey>();

            if (TryGetKeyValuePair(relationalMappings, mappedValue, out var keyValuePair))
            {
                _logger.LogInformation("Found mapping for {0}. Value is {1}", mappedValue, keyValuePair.Value);
                return keyValuePair;
            }

            var key = await getKeyFunc(mappedValue);

            var addedKeyValuePair = AddMapping(relationalMappings, mappedValue, key);

            if (!addedKeyValuePair.HasValue)
                return default;

            RelationalMappings = relationalMappings;

            _logger.LogInformation("Cached new mapping for {0}. Value is {1}", mappedValue, key);

            return keyValuePair;
        }

        internal bool TryGetKeyValuePair(IDictionary<TMap, TKey> relationalMappings, TMap mappedValue, out KeyValuePair<TMap, TKey> keyValuePair)
        {
            keyValuePair = new KeyValuePair<TMap, TKey>();

            if (!relationalMappings.ContainsKey(mappedValue) || !relationalMappings
                    .TryGetValue(mappedValue, out var key)) return false;
            keyValuePair = new KeyValuePair<TMap, TKey>(mappedValue, key);
            
            return true;

        }

        internal KeyValuePair<TMap, TKey>? AddMapping(IDictionary<TMap, TKey> relationalMappings, TMap mappedValue, TKey key)
        {
            return relationalMappings.TryAdd(mappedValue, key) 
                ? new KeyValuePair<TMap, TKey>(mappedValue, key) 
                : default;
        }

        internal IDictionary<TMap, TKey> RelationalMappings
        {
            get => _cacheProvider.Get<IDictionary<TMap, TKey>>(CacheType.DistributedCache, _cacheName).Result;
            set => _cacheProvider.Set(CacheType.DistributedCache, _cacheName, value).Wait();
        }
    }
}