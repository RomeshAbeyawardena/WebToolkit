using System;
using System.Threading.Tasks;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Extensions
{
    public static class CacheProviderExtensions
    {
        public static async Task<T> LoadAsync<T>(this ICacheProvider cacheProvider, CacheType cacheType, string key, Func<Task<T>> loader)
        {
            var result = await cacheProvider.Get<T>(cacheType, key);

            if (result != null)
                return result;

            result = await loader();
            await cacheProvider.Set(cacheType, key, result);

            return result;
        }
    }
}