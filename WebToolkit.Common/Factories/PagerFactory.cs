using System;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Factories;

namespace WebToolkit.Common.Factories
{
    public class PagerFactory : IPagerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public IPager<TModel> GetPager<TModel>()
        {
            return _serviceProvider.GetRequiredService<IPager<TModel>>();
        }

        public PagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}