﻿using System.Collections.Generic;

namespace WebToolkit.Contracts.Providers
{
    public interface IMapperProvider
    {
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    }
}