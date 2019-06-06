using System.Collections.Generic;

namespace WebToolkit.Contracts.Builders
{
    public interface IListBuilder<T> : IEnumerable<T>
    {
        T this[int index] { get; }
        IListBuilder<T> Add(T entry);
        IListBuilder<T> AddRange(IEnumerable<T> entry);
        bool Contains(T entry);
        IList<T> ToList();
        IEnumerable<T> ToArray();
    }
}