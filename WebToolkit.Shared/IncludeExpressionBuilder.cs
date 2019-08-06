using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Shared
{
    public class IncludeExpressionBuilder<TModel>
    {
        private readonly IList<IIncludeExpression<TModel>> _includeExpressions;

        private IncludeExpressionBuilder(IEnumerable<IIncludeExpression<TModel>> includeExpressions = null)
        {
            _includeExpressions = includeExpressions == null 
                ? new List<IIncludeExpression<TModel>>() 
                : new List<IIncludeExpression<TModel>>(includeExpressions);
        }

        public IncludeExpressionBuilder<TModel> AddExpression(IIncludeExpression<TModel> expression)
        {
            _includeExpressions.Add(expression);
            return this;
        }
        public IncludeExpressionBuilder<TModel> AddExpression<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            AddExpression(IncludeExpression<TModel, TKey>.Create(expression));
            return this;
        }

        public IEnumerable<IIncludeExpression<TModel>> ToArray() => _includeExpressions.ToArray();

        public static IncludeExpressionBuilder<TModel> CreateBuilder(IEnumerable<IIncludeExpression<TModel>> includeExpressions = null)
        {
            return new IncludeExpressionBuilder<TModel>(includeExpressions);
        }
    }
}