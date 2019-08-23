using System.Collections.Generic;

namespace WebToolkit.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<T> CreateAdd<T>(this ICollection<T> valueCollection, T value)
        {
            return CreateAdd(valueCollection, new[] {value});
        }
        public static ICollection<T> CreateAdd<T>(this ICollection<T> valueCollection, IEnumerable<T> value)
        {
            if(valueCollection == null)
                valueCollection = new List<T>();

            value.ForEach(item => valueCollection.Add(item));

            return valueCollection;
        }
    }
}