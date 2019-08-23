using System.Collections.Generic;
using System.Data;

namespace WebToolkit.Contracts
{
    public interface IDataSetTransformer
    {
        IEnumerable<TDestination> TransformDataSet<TDestination>(DataSet dataSet, string columnOrder, bool includesHeaders = false);
    }
}