using System;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common
{
    public class DefaultValuesFactory : IDefaultValuesFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public void Assign<TModel>(TModel model)
        {
            var genericDefaultValueProvider = typeof(IDefaultValueProvider<>);
            var defaultValueProvider = genericDefaultValueProvider.MakeGenericType(typeof(TModel));
            
            var valueProvider = (IDefaultValueProvider) _serviceProvider.GetService(defaultValueProvider);

            if(valueProvider == null)
                throw new NullReferenceException($"Unable to find a DefaultValuesProvider for {typeof(TModel)}");

            valueProvider.Assign(model);
        }

        public DefaultValuesFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}