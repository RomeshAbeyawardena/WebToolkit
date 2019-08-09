using System;
using System.Collections.Generic;
using AutoMapper;
using WebToolkit.Contracts.Providers;

namespace WebToolkit.Common.Providers
{
    public sealed class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

        public object Map(object value, Type sourceType, Type destinationType)
        {
            return _mapper.Map(value, sourceType, destinationType);
        }

        public object MapArray(object value, Type sourceType, Type destinationType)
        {
            var enumerableGeneric = typeof(IEnumerable<>);
            var sourceEnumerable = enumerableGeneric.MakeGenericType(sourceType);
            var destinationEnumerable = enumerableGeneric.MakeGenericType(destinationType);
            var mappedValue = Map(value, sourceEnumerable, destinationEnumerable);

            return mappedValue;
        }

        public MapperProvider(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}