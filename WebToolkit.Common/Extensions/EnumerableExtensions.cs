using System;
using System.Collections.Generic;
using System.Linq;

namespace WebToolkit.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action, Func<T, bool> whereExpression = null)
        {
            foreach (var item in whereExpression == null 
                ? items 
                : items.Where(whereExpression))
            {
                action(item);
            }
        }
    }
}