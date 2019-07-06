using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IJSonSettings _jsonSettings;
        private readonly IDistributedCache _distributedCache;
        private readonly IAsyncLockDictionary _asyncLockDictionary;

        public CacheProvider(IJSonSettings jsonSettings, IDistributedCache distributedCache, IAsyncLockDictionary asyncLockDictionary)
        {
            _keyEntries = new List<string>();
            _jsonSettings = jsonSettings;
            _distributedCache = distributedCache;
            _asyncLockDictionary = asyncLockDictionary;
        }

        public async Task<T> Get<T>(CacheType cacheType, string key)
        {
            return await _asyncLockDictionary.GetOrCreate("Get", async () =>
            {
                if (!_keyEntries.Contains(key))
                    return default;
                var result = await _distributedCache.GetStringAsync(key, CancellationToken.None);
                return result == null ? default : JToken.Parse(result, _jsonSettings.LoadSettings).ToObject<T>();
            }).Invoke();
        }

        public async Task Set<T>(CacheType cacheType, string key, T value)
        {
            await _asyncLockDictionary.GetOrCreate("Set", async () =>
            {
                _keyEntries.Add(key);
                var val = JToken.FromObject(value, _jsonSettings.Serializer).ToString();
                await _distributedCache.SetStringAsync(key, val);
            }).Invoke();
        }

        public async Task<int> Clear()
        {
            return await _asyncLockDictionary.GetOrCreate("Clear", async () =>
            {
                var keyCount = _keyEntries.Count;
                await _keyEntries.ForEach(async (key) => await _distributedCache.RemoveAsync(key));
                _keyEntries.Clear();

                return keyCount;
            }).Invoke();
        }

        public async Task ClearByKey(string cacheKey)
        {
            await _asyncLockDictionary.GetOrCreate("ClearByKey",
                async () => { _keyEntries.Remove(cacheKey);
                    await _distributedCache.RemoveAsync(cacheKey);
                }).Invoke();
        }


        private readonly IList<string> _keyEntries;
    }
}