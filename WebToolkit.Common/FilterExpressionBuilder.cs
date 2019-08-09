using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebToolkit.Common
{
    public sealed class FilterExpressionBuilder
    {
        public static FilterExpressionBuilder<TModel> CreateBuilder<TModel>(
            IDictionary<string, object> filterDictionary)
        {
            return FilterExpressionBuilder<TModel>.CreateBuilder(filterDictionary);
        }
    }

    public class FilterExpressionBuilder<TModel>
    {
        private readonly IDictionary<string, object> _filterDictionary;

        private FilterExpressionBuilder(IDictionary<string, object> filterDictionary)
        {
            _filterDictionary = filterDictionary;
        }

        private Expression<Func<TModel, bool>> GetFilterExpression(IDictionary<string, object> filterDictionary)
        {
            foreach (var o in filterDictionary)
            {
                
            }
            return new Expression<Func<TModel, bool>>();
        }

        public static FilterExpressionBuilder<TModel> CreateBuilder(IDictionary<string, object> filterDictionary)
        {
            return new FilterExpressionBuilder<TModel>(filterDictionary);
        }

        public Expression<Func<TModel, bool>> FilterExpression => GetFilterExpression(_filterDictionary);
    }
}