using System;

namespace WebToolkit.Contracts
{
    public interface ICustomMapper<TSource, TDestination, T>
    {
        ISwitch<T, Func<TSource, TDestination>> CustomMapperSwitch { get; }
        TDestination Map(T type, TSource source);
    }
}