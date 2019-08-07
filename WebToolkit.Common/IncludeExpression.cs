using System;
using System.Linq.Expressions;
using WebToolkit.Common.Extensions;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Shared
{
    public class IncludeExpression<TModel, TKey> : IIncludeExpression<TModel>
    {
        private IncludeExpression(Expression<Func<TModel, TKey>> value)
        {
            Value = value;
        }

        public Expression<Func<TModel, TKey>> Value { get; }

        public static IIncludeExpression<TModel> Create(Expression<Func<TModel, TKey>> value)
        {
            return new IncludeExpression<TModel, TKey>(value);
        }

        Expression<Func<TModel, object>> IIncludeExpression<TModel>.Value => Value.ToUntypedPropertyExpression();
    }
}