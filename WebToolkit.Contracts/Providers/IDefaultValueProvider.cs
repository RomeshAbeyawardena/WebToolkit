using System;

namespace WebToolkit.Contracts.Providers
{
    public interface IDefaultValueProvider
    {
        void Assign(object model);
        Action<object> Defaults { get; }
    }
    public interface IDefaultValueProvider<in TModel> : IDefaultValueProvider
    {
        new Action<TModel> Defaults { get; }
        void Assign(TModel model);
    }
}