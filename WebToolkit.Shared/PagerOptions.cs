using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebToolkit.Shared.Contracts;

namespace WebToolkit.Shared
{
    public class PagerOptions<TModel, TKey>
    {
        public Expression<Func<TModel, bool>> FilterExpression { get; set; }
        public Expression<Func<TModel, TKey>> OrderByExpression { get; set; }
        public IEnumerable<IIncludeExpression<TModel>> IncludeExpressions { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}