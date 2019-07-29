using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Factories;

namespace WebToolkit.Common.Factories
{
    public class RelationalMapperFactory : IRelationalMapperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private IRelationalMapper<TEntity, TKey, TMap> GetRelationalMapper<TEntity, TMap, TKey>()
        {
            var relationalMapperType = typeof(IRelationalMapper<TEntity, TKey, TMap>);
            return (IRelationalMapper<TEntity, TKey, TMap>)_serviceProvider
                .GetRequiredService(relationalMapperType);
        }

        public RelationalMapperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public KeyValuePair<TMap, TKey> GetOrCreate<TEntity, TMap, TKey>(TMap mappedValue, Func<TMap, TKey> getKeyFunc)
        {
            var relationshipMapper = GetRelationalMapper<TEntity, TMap, TKey>();

            return relationshipMapper
                .GetOrCreate(mappedValue, getKeyFunc);
        }

        public Task<KeyValuePair<TMap, TKey>> GetOrCreateAsync<TEntity, TMap, TKey>(TMap mappedValue, Func<TMap, Task<TKey>> getKeyFunc)
        {
            var relationshipMapper = GetRelationalMapper<TEntity, TMap, TKey>();

            return relationshipMapper
                .GetOrCreateAsync(mappedValue, getKeyFunc);
        }
    }
}