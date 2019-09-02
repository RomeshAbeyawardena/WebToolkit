using System.Collections.Generic;
using System.Data;
using WebToolkit.Shared;

namespace WebToolkit.Contracts
{
    public interface IDataSetTransformer
    {
        IEnumerable<TDestination> TransformDataSet<TDestination>(DataTable dataTable, IEnumerable<OrderedColumnInfo> columnOrder, bool includesHeaders = false);
    }
}