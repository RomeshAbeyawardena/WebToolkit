using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebToolkit.Common.Builders;
using WebToolkit.Contracts.Builders;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public sealed class FilterExpressionBuilder
    {
        public static FilterExpressionBuilder<TModel> CreateBuilder<TModel>(
            IDictionary<string, FilterExpressionParameter> filterDictionary)
        {
            return FilterExpressionBuilder<TModel>.CreateBuilder(filterDictionary);
        }

        public static FilterExpressionBuilder<TModel> CreateBuilder<TModel>(Action<IDictionaryBuilder<string, FilterExpressionParameter>> filterExpressionDictionary)
        {
            var filterExpressionDictionaryInstance =
                DictionaryBuilder.CreateBuilder<string, FilterExpressionParameter>();

            filterExpressionDictionary(filterExpressionDictionaryInstance);

            return CreateBuilder<TModel>(filterExpressionDictionaryInstance.ToDictionary());
        }
    }

    public class FilterExpressionBuilder<TModel>
    {
        private readonly IDictionary<string, FilterExpressionParameter> _filterDictionary;

        private FilterExpressionBuilder(IDictionary<string, FilterExpressionParameter> filterDictionary)
        {
            _filterDictionary = filterDictionary;
        }

        private static Expression GetUnaryExpressionByOperator(Operator operatorValue, Expression leftExpression, Expression rightExpression = null)
        {
            switch (operatorValue)
            {
                case Operator.And:
                    return Expression.And(leftExpression, rightExpression ?? throw new ArgumentNullException(nameof(rightExpression)));
                case Operator.Not:
                    if (rightExpression == null)
                        return Expression.Not(leftExpression);

                    return Expression.And(leftExpression, Expression.Not(rightExpression));
                case Operator.Or:
                    return Expression.Or(leftExpression, rightExpression  
                                                         ?? throw new ArgumentNullException(nameof(rightExpression)));
                default:
                    throw new FormatException();
            }
        }

        private static Expression GetBinaryExpressionByComparisonType(ComparisonType comparisonType, Expression leftExpression, Expression rightExpression)
        {
            if (comparisonType.HasFlag(ComparisonType.Equal | ComparisonType.GreaterThan))
                return Expression.GreaterThanOrEqual(leftExpression, rightExpression);

            if (comparisonType.HasFlag(ComparisonType.Equal | ComparisonType.LessThan))
                return Expression.LessThanOrEqual(leftExpression, rightExpression);

            switch (comparisonType)
            {
                case ComparisonType.Equal:
                    return Expression.Equal(leftExpression, rightExpression ?? throw new ArgumentNullException(nameof(rightExpression)));
                case ComparisonType.GreaterThan:
                    return Expression.GreaterThan(leftExpression, rightExpression ?? throw new ArgumentNullException(nameof(rightExpression)));
                case ComparisonType.LessThan:
                    return Expression.LessThan(leftExpression, rightExpression ?? throw new ArgumentNullException(nameof(rightExpression)));
                case ComparisonType.Contains:
                    return Expression.Call(leftExpression, "Contains", null, rightExpression);
                default:
                    throw new InvalidOperationException();
            }
        }

        private Expression<Func<TModel, bool>> GetFilterExpression (IDictionary<string, FilterExpressionParameter> filterDictionary)
        {
            var modelType = typeof(TModel);
            var objectType = typeof(object);
            var expressionParameter = Expression.Parameter(modelType, "model");
            Expression assignedExpression = null;
            foreach (var filter in filterDictionary)
            {
                var filterExpressionParameter = filter.Value;
                var propertyExpression = Expression.PropertyOrField(expressionParameter, filter.Key);
                var constantExpression = Expression.Constant(filterExpressionParameter.Value, filterExpressionParameter.ValueType);
                var equalityExpression = GetBinaryExpressionByComparisonType(filterExpressionParameter.ComparisonType, 
                    propertyExpression, constantExpression);

                if (assignedExpression == null)
                {
                    assignedExpression = filterExpressionParameter.Operator == Operator.Not 
                        ? GetUnaryExpressionByOperator(Operator.Not, equalityExpression)
                        : equalityExpression;
                    continue;
                }

                assignedExpression = GetUnaryExpressionByOperator(filterExpressionParameter.Operator,
                    assignedExpression, equalityExpression);
            }

            var expression = Expression.Lambda<Func<TModel, bool>>(assignedExpression, expressionParameter);
            return expression;
        }

        public static FilterExpressionBuilder<TModel> CreateBuilder(IDictionary<string, FilterExpressionParameter> filterDictionary)
        {
            return new FilterExpressionBuilder<TModel>(filterDictionary);
        }

        public Expression<Func<TModel, bool>> FilterExpression => GetFilterExpression(_filterDictionary);
    }
}