using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebToolkit.Contracts.Builders;

namespace WebToolkit.Common.Builders
{
    public sealed class ListBuilder<T> : IListBuilder<T>
    {
        private IEnumerator<T> Enumerator => _internalList.GetEnumerator();
        private readonly IList<T> _internalList;
        private ListBuilder(IEnumerable<T> items)
        {
            _internalList = new List<T>(items);
        }

        public static IListBuilder<T> Create(IEnumerable<T> items)
        {
            return new ListBuilder<T>(items);
        }

        public T this[int index] => _internalList[index];

        public IListBuilder<T> Add(T entry)
        {
            _internalList.Add(entry);
            return this;
        }

        public IListBuilder<T> AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }

            return this;
        }

        public bool Contains(T entry)
        {
            return _internalList.Contains(entry);
        }

        public IList<T> ToList()
        {
            return _internalList.ToList();
        }

        public IEnumerable<T> ToArray()
        {
            return _internalList.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}