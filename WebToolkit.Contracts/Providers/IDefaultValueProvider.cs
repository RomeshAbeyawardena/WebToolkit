using System;

namespace WebToolkit.Contracts.Providers
{
    public interface IDefaultValueProvider<in TModel>
    {
        Action<TModel> Defaults { get; }
        void Assign(TModel model);
    }
}