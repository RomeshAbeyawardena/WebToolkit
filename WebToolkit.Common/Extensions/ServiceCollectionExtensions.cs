﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
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

        public static TService GetRequiredService<TService>(this IServiceCollection services)
        {
            return services.BuildServiceProvider()
                .GetRequiredService<TService>();
        }

        public static IServiceCollection RegisterServicesFromAssemblies<TServiceRegistration, TServiceBroker>(
            this IServiceCollection services, Func<TServiceBroker, IEnumerable<Assembly>> getAssemblies,
            Action<TServiceRegistration, IServiceCollection> registerServices) 
            where TServiceRegistration : class
        {
            foreach (var a in getAssemblies(Activator.CreateInstance<TServiceBroker>()))
            {
                foreach (var t in a.GetTypes().Where(type => type.IsClass 
                                                             && type.GetInterface(nameof(IServiceRegistration)) == typeof(TServiceRegistration)))
                {
                    var serviceRegistration = Activator.CreateInstance(t) as TServiceRegistration;
                    registerServices(serviceRegistration, services);
                }
            }

            return services;
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection service)
        {
            return service
                .AddSingleton<ISystemClock, SystemClock>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
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