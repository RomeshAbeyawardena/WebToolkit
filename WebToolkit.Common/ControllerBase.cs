using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public abstract class ControllerBase : Controller
    {
        private IMapperProvider MapperProvider => GetRequiredService<IMapperProvider>();

        protected virtual TServiceImplementation GetRequiredService<TServiceImplementation>()
        {
            if (HttpContext == null)
                throw new InvalidOperationException("HttpContext unavailable.");
            return HttpContext
                .RequestServices.GetRequiredService<TServiceImplementation>();
        }

        protected virtual async Task<T> LoadAsync<T>(CacheType cacheType, string key, Func<Task<T>> loader)
        {
            var cacheProvider = GetRequiredService<ICacheProvider>()
                .LoadAsync(cacheType, key, loader);
        }

        public TDestination Map<TSource, TDestination>(TSource source) => MapperProvider.Map<TSource, TDestination>(source);
        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) => MapperProvider.Map<TSource, TDestination>(source);
    }
}