using System;
using System.Collections.Generic;
using System.Linq;
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

        private Expression<Func<TModel, bool>> GetFilterExpression (IDictionary<string, object> filterDictionary)
        {
            var modelType = typeof(TModel);
            var objectType = typeof(object);
            var expressionParameter = Expression.Parameter(modelType, "model");

            var assignedExpression = (from o in filterDictionary
                let body = Expression.PropertyOrField(expressionParameter, o.Key)
                let constant = Expression.Constant(o.Value)
                select Expression.Equal(body, constant))
                    .Aggregate<BinaryExpression, Expression>(null, (current, equateExpression) => current == null
                ? equateExpression
                : Expression.And(current, equateExpression));

            var expression = Expression.Lambda<Func<TModel, bool>>(assignedExpression, expressionParameter);
            return expression;
        }

        public static FilterExpressionBuilder<TModel> CreateBuilder(IDictionary<string, object> filterDictionary)
        {
            return new FilterExpressionBuilder<TModel>(filterDictionary);
        }

        public Expression<Func<TModel, bool>> FilterExpression => GetFilterExpression(_filterDictionary);
    }
}