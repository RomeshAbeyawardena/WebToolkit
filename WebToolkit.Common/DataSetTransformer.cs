using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class DataSetTransformer : IDataSetTransformer
    {
        public IEnumerable<TDestination> TransformDataSet<TDestination>(DataSet dataSet, string columnOrder, bool includesHeaders = false)
        {
            var table0 = dataSet.Tables[0];
            var placeHolderList = new List<TDestination>();

            var placeHolderType = typeof(TDestination);

            var columns = columnOrder.Split(new [] {"|"}, StringSplitOptions.RemoveEmptyEntries);

            var columnDictionary = new Dictionary<int, PropertyInfo>();
            for (var i=0; i< columns.Length; i++)
            {
                var column = columns[i];
                columnDictionary.Add(i, placeHolderType.GetProperty(column));
            }
            
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