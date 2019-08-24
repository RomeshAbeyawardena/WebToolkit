using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public class DataSetTransformer : IDataSetTransformer
    {
        public IEnumerable<TDestination> TransformDataSet<TDestination>(DataSet dataSet, IEnumerable<OrderedColumnInfo> columnOrder, bool includesHeaders = false)
        {
            var table0 = dataSet.Tables[0];
            var placeHolderList = new List<TDestination>();

            var placeHolderType = typeof(TDestination);

            var columnDictionary = new Dictionary<int, PropertyInfo>();

            columnOrder.ForEach(c => columnDictionary.Add(c.Index, placeHolderType.GetProperty(c.Name)));

            for (var i=0;i < table0.Rows.Count; i++)
            {
                var placeHolder = Activator.CreateInstance<TDestination>();
                var row = table0.Rows[i];

                if (includesHeaders && i == 0)
                    continue;

                foreach (var column in columnDictionary)
                {
                    var data = row[column.Key];

                    if (data == null)
                        continue;

                    if (data.GetType() != column.Value.PropertyType)
                        data = Convert.ChangeType(data, column.Value.PropertyType);

                    column.Value.SetValue(placeHolder, data);
                }

                placeHolderList.Add(placeHolder);
            }

            return placeHolderList.ToArray();
        }
    }
}