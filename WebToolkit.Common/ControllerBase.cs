using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public class ControllerBase : Controller
    {
        private IMapperProvider MapperProvider => GetRequiredService<IMapperProvider>();

        protected TServiceImplementation GetRequiredService<TServiceImplementation>()
        {
            if (HttpContext == null)
                throw new InvalidOperationException("HttpContext unavailable.");
            return HttpContext
                .RequestServices.GetRequiredService<TServiceImplementation>();
        }

        protected async Task<T> LoadAsync<T>(CacheType cacheType, string key, Func<Task<T>> loader)
        {
            var cacheProvider = GetRequiredService<ICacheProvider>();
            var result = await cacheProvider.Get<T>(cacheType, key);

            if (result != null)
                return result;

            result = await loader();
            await cacheProvider.Set(cacheType, key, result);

            return result;
        }

        public TDestination Map<TSource, TDestination>(TSource source) => MapperProvider.Map<TSource, TDestination>(source);
        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) => MapperProvider.Map<TSource, TDestination>(source);
    }
}