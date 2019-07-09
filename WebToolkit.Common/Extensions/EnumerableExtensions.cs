using System;
using System.Collections.Generic;
using System.Linq;

namespace WebToolkit.Common.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Runs a single Action against the items of an IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">IEnumerable to perform a supplied action on</param>
        /// <param name="action">Action to perform against items of IEnumerable</param>
        /// <param name="whereExpression">Filter expression to filter items to perform the supplied action against</param>
        /// <returns>A new instance of IEnumerable with processed array values. </returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Func<T, T> action, Func<T, bool> whereExpression = null)
        {
            var filteredItems = (whereExpression == null
                ? items
                : items.Where(whereExpression)).ToArray();

            for (var i = 0; i < filteredItems.Length; i++)
            {
                var currentItem = filteredItems[i];
                filteredItems[i] = action(currentItem);
            }

            return filteredItems;
        }

        /// <summary>
        /// Runs a single Action against the items of an IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">IEnumerable to perform a foreach action on</param>
        /// <param name="action">Action to perform against items of IEnumerable</param>
        /// <param name="whereExpression">Filter expression to filter items to perform the supplied action against</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action, Func<T, bool> whereExpression = null)
        {
            var filteredItems = (whereExpression == null
                ? items
                : items.Where(whereExpression)).ToArray();

            foreach (var currentItem in filteredItems)
            {
                action(currentItem);
            }
        }
    }
}