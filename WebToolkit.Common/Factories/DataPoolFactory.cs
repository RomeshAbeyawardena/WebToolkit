﻿using System;
using System.Threading.Tasks;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Factories;

namespace WebToolkit.Common.Factories
{
    public class DataPoolFactory : IDataPoolFactory
    {
        private readonly IServiceProvider _serviceProvider;

        internal IDataPool<TEntity, TKey> GetDataPool<TEntity, TKey>()
        {
            var dataPoolType = typeof(IDataPool<TEntity, TKey>);
            return (IDataPool<TEntity, TKey>) _serviceProvider.GetService(dataPoolType);
        }

        public DataPoolFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TEntity GetValue<TEntity, TKey>(TKey key, Func<TKey, TEntity> valueFunc)
        {
            var dataPool = GetDataPool<TEntity, TKey>();

            var value = dataPool.Retrieve(key);
            
            return value == null 
                ? dataPool.Add(key, valueFunc(key))
                : value;
        }

        public async Task<TEntity> GetValueAsync<TEntity, TKey>(TKey key, Func<TKey, Task<TEntity>> valueFunc)
        {
            var dataPool = GetDataPool<TEntity, TKey>();

            var value = dataPool.Retrieve(key);
            
            return value == null 
                ? dataPool.Add(key, await valueFunc(key))
                : value;
        }
    }
}