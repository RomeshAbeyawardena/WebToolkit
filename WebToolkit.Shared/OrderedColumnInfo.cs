using System.Collections.Generic;
using System.Linq;

namespace WebToolkit.Shared
{
    public struct OrderedColumnInfo
    {
        public OrderedColumnInfo(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; set; }
        public string Name { get; set; }

        public static IEnumerable<OrderedColumnInfo> GetOrderedColumns(string[] columns)
        {
            return columns.Select((t, i) => new OrderedColumnInfo(i, t)).ToArray();
        }
    }
}