using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public class DataSetTransformer : IDataSetTransformer
    {
        public IEnumerable<TDestination> TransformDataSet<TDestination>(DataTable dataTable, 
            IEnumerable<OrderedColumnInfo> columnOrder, bool includesHeaders = false)
        {
            var destinationList = new List<TDestination>();

            var destinationType = typeof(TDestination);

            var columnDictionary = new Dictionary<int, PropertyInfo>();

            columnOrder.ForEach(c => columnDictionary.Add(c.Index, destinationType.GetProperty(c.Name)));

            for (var i=0;i < dataTable.Rows.Count; i++)
            {
                var converted = Activator.CreateInstance<TDestination>();
                var row = dataTable.Rows[i];

                if (includesHeaders && i == 0)
                    continue;

                foreach (var column in columnDictionary)
                {
                    var data = row[column.Key];

                    if (data == null)
                        continue;

                    if (data.GetType() != column.Value.PropertyType)
                        data = Convert.ChangeType(data, column.Value.PropertyType);

                    column.Value.SetValue(converted, data);
                }

                destinationList.Add(converted);
            }

            return destinationList.ToArray();
        }
    }
}