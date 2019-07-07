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

        public MapperProvider(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}