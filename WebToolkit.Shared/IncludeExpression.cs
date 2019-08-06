using System;
using System.Linq.Expressions;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Shared
{
    public class IncludeExpression<TModel, TKey> : IIncludeExpression<TModel>
    {
        private IncludeExpression(Func<TModel, TKey> value)
        {
            Value = value;
        }

        public Func<TModel, TKey> Value { get; }

        public static IIncludeExpression<TModel> Create(Func<TModel, TKey> value)
        {
            return new IncludeExpression<TModel, TKey>(value);
        }

        Expression<Func<TModel, object>> IIncludeExpression<TModel>.Value => Value as Expression<Func<TModel, object>>;
    }
}