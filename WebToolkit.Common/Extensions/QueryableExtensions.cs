using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TModel> IncludeMany<TModel>(this IQueryable<TModel> query,
            IEnumerable<IIncludeExpression<TModel>> includeExpression)
            where TModel: class
        {
            return includeExpression.ToArray()
                .Aggregate(query, (current, optionsIncludeExpression) => current.Include(optionsIncludeExpression.Value));
        }

        public static IQueryable<TModel> IncludeMany<TModel>(this IQueryable<TModel> query,
            Action<IncludeExpressionBuilder<TModel>> builder)
            where TModel: class
        {
            var includeExpressionBuilder = IncludeExpressionBuilder<TModel>.CreateBuilder();
            builder(includeExpressionBuilder);

            return IncludeMany(query, includeExpressionBuilder.ToArray());
        }
    }
}