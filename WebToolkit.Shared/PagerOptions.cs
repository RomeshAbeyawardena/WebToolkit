using System;
using System.Linq.Expressions;

namespace WebToolkit.Shared
{
    public class PagerOptions<TModel, TKey>
    {
        public Expression<Func<TModel, bool>> FilterExpression { get; set; }
        public Expression<Func<TModel, TKey>> OrderByExpression { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}