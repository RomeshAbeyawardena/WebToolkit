using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Common.Providers;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Data;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories<TDbContext>(this IServiceCollection services, params Type[] entities) where TDbContext : DbContext
        {
            foreach (var entity in entities)
            {
                var repositoryDefinitionType = typeof(IRepository<>);
                var repositoryType = typeof(DefaultRepository<,>);

                var genericRepositoryDefinitionType = repositoryDefinitionType.MakeGenericType(entity);
                var genericRepositoryType = repositoryType.MakeGenericType(typeof(TDbContext), entity);

                services.AddScoped(genericRepositoryDefinitionType, genericRepositoryType);
            }

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection service)
        {
            return service.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IMapperProvider, MapperProvider>()
                .AddSingleton<ICryptographyProvider, CryptographyProvider>()
                .AddSingleton<ICacheProvider, CacheProvider>()
                .AddSingleton<IAsyncLockDictionary, DefaultAsyncLockDictionary>(); 
        }

        public static IServiceCollection AddDefaultValueProvider<TModel>(this IServiceCollection services, Action<TModel> defaults)
        {
            return services.AddSingleton<IDefaultValueProvider<TModel>>(new DefaultValuesProvider<TModel>(defaults));
        }

        
        public static IServiceCollection AddDefaultValueProvider<TModel>(this IServiceCollection services)
        {
            return services.AddSingleton<IDefaultValueProvider<TModel>>(new DefaultValuesProvider<TModel>(model => {}));
        }
    }
}