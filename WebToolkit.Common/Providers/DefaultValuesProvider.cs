using System;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Providers
{
    public class DefaultValuesProvider<TModel> : IDefaultValueProvider<TModel>
    {
        private DefaultValuesProvider(Action<TModel> defaults)
        {
            Defaults = defaults;
        }

        public static IDefaultValueProvider<TModel> Create(Action<TModel> defaultValues)
        {
            return new DefaultValuesProvider<TModel>(defaultValues);
        }

        void IDefaultValueProvider.Assign(object model)
        {
            Assign((TModel)model);
        }

        Action<object> IDefaultValueProvider.Defaults => throw new NotSupportedException();

        public Action<TModel> Defaults { get; }
        public void Assign(TModel model)
        {
            Defaults(model);
        }
    }
}