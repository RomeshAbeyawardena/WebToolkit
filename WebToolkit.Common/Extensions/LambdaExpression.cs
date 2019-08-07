using System;
using System.Linq.Expressions;

namespace WebToolkit.Common.Extensions
{
    public static class LambdaExpression
    {
        public static Expression<Func<TInput, object>> ToUntypedPropertyExpression<TInput, TOutput> (this Expression<Func<TInput, TOutput>> expression)
        {
            var memberName = ((MemberExpression)expression.Body).Member.Name;

            var param = Expression.Parameter(typeof(TInput));
            var field = Expression.Property(param, memberName);
            return Expression.Lambda<Func<TInput, object>>(field, param);
        }
    }
}