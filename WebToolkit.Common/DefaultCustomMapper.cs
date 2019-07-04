using System;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public sealed class DefaultCustomMapper<TSource, TDestination, T> : ICustomMapper<TSource, TDestination, T>
    {
        public ISwitch<T, Func<TSource, TDestination>> CustomMapperSwitch { get; }
        public TDestination Map(T type, TSource source)
        {
            return CustomMapperSwitch.Case(type).Invoke(source);
        }

        public DefaultCustomMapper(ISwitch<T, Func<TSource, TDestination>> customMapperSwitch)
        {
            CustomMapperSwitch = customMapperSwitch;
        }
    }
}