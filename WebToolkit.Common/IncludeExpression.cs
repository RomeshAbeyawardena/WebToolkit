﻿using System;
using System.Linq.Expressions;
using WebToolkit.Common.Extensions;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Common
{
    public sealed class IncludeExpression
    {
        public static IIncludeExpression<TModel> Create<TModel, TKey>(Expression<Func<TModel, TKey>> value)
        {
            return IncludeExpression<TModel, TKey>.Create(value);
        }
    }

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