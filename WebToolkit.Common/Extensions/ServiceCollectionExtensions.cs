﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using WebToolkit.Common.Factories;
using WebToolkit.Common.Providers;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Data;
using WebToolkit.Contracts.Factories;
using WebToolkit.Contracts.Providers;
using Encoding = System.Text.Encoding;

namespace WebToolkit.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPager<TModel>(this IServiceCollection services) where TModel : class
        {
            return services.RegisterPagers(typeof(TModel));
        }

        public static IServiceCollection RegisterPagers(this IServiceCollection services, params Type[] types)
        {
            var serviceType = typeof(IPager<>);
            var implementationType = typeof(Pager<>);

            foreach (var type in types)
            {
                services.AddScoped(serviceType.MakeGenericType(type), implementationType.MakeGenericType(type));
            }

            return services;
        }

        public static IServiceCollection RegisterDataPool<TEntity, TKey>(this IServiceCollection services)
        {
            return services.AddSingleton<IDataPool<TEntity, TKey>, DataPool<TEntity, TKey>>();
        }

        public static IServiceCollection RegisterRelationalMapper<TEntity, TKey, TMap>(this IServiceCollection services)
        {
            return services.AddSingleton<IRelationalMapper<TEntity, TKey, TMap>, RelationalMapper<TEntity, TKey, TMap>>();
        }

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
                foreach (var t in a.GetTypes().Where(type => type
                    .ClassInherits<IServiceRegistration>(typeof(TServiceRegistration))))
                {
                    var serviceRegistration = Activator.CreateInstance(t) as TServiceRegistration;
                    registerServices(serviceRegistration, services);
                }
            }

            return services;
        }

        public static IServiceCollection RegisterServicesFromAssemblies<TServiceBroker>(this IServiceCollection services)
            where TServiceBroker : IServiceBroker
        {
            return RegisterServicesFromAssemblies<IServiceRegistration, TServiceBroker>(services, serviceProvider =>
                serviceProvider.GetServiceAssemblies(), (serviceRegistration, s) => serviceRegistration.RegisterServices(s));
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection service)
        {
            return service
                .AddSingleton<IPagerFactory, PagerFactory>()
                .AddSingleton<IFileProvider, FileProvider>()
                .AddSingleton<IMimeTypeProvider, MimeTypeProvider>()
                .AddSingleton(Switch<Contracts.Providers.Encoding, Encoding>
                    .Create(defaultValueExpression: () => default(Encoding))
                    .CaseWhen(Contracts.Providers.Encoding.Ascii, Encoding.ASCII)
                    .CaseWhen(Contracts.Providers.Encoding.BigEndianUnicode, Encoding.BigEndianUnicode)
                    .CaseWhen(Contracts.Providers.Encoding.Utf32, Encoding.UTF32)
                    .CaseWhen(Contracts.Providers.Encoding.Utf7, Encoding.UTF7)
                    .CaseWhen(Contracts.Providers.Encoding.Utf8, Encoding.UTF8)
                    .CaseWhen(Contracts.Providers.Encoding.Unicode, Encoding.Unicode))
                .AddSingleton<IDefaultValuesFactory, DefaultValuesFactory>()
                .AddSingleton<IDataPoolFactory, DataPoolFactory>()
                .AddSingleton<IRelationalMapperFactory, RelationalMapperFactory>()
                .AddSingleton<IEncodingProvider, EncodingProvider>()
                .AddSingleton<ISystemClock, SystemClock>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IMapperProvider, MapperProvider>()
                .AddSingleton<ICryptographyProvider, CryptographyProvider>()
                .AddSingleton<ICacheProvider, CacheProvider>()
                .AddSingleton<IAsyncLockDictionary, DefaultAsyncLockDictionary>()
                .AddSingleton<IConnectionStringBuilder, ConnectionStringBuilder>()
                .AddSingleton<IArgumentParser, ArgumentParser>()
                .AddSingleton<IExcelDocumentParser, ExcelDocumentParser>()
                .AddSingleton<IDataSetTransformer, DataSetTransformer>();
        }

        public static IServiceCollection AddDefaultValueProvider<TModel>(this IServiceCollection services, Action<TModel> defaults)
        {
            return services.AddSingleton(DefaultValuesProvider<TModel>.Create(defaults));
        }

        
        public static IServiceCollection AddDefaultValueProvider<TModel>(this IServiceCollection services)
        {
            return services.AddSingleton(DefaultValuesProvider<TModel>.Create(model => {}));
        }
    }
}