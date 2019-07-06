﻿using System.Collections.Generic;

namespace WebToolkit.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> CreateAdd<T>(this ICollection<T> valueCollection, T value)
        {
            if(valueCollection == null)
                valueCollection = new List<T>();

            valueCollection.Add(value);

            return valueCollection;
        }
    }
}