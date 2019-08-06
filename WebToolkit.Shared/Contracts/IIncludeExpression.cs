using System;
using System.Linq.Expressions;

namespace WebToolkit.Shared.Contracts
{
    public interface IIncludeExpression<TModel>
    {
        Expression<Func<TModel, object>> Value { get; }
    }
}