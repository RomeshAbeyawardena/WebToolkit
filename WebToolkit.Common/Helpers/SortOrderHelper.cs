using System;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace WebToolkit.Common.Helpers
{
    public static class SortOrderHelper
    {
        public static dynamic GenerateSortOrder<TModel>(IPagedOrderedRequest viewModel, out OrderBy orderBy)
        {
            orderBy = OrderBy.Ascending;

            if (string.IsNullOrEmpty(viewModel.SortKey))
                return ExpressionBuilder.Generate<TModel>("Id");

            var sortOrderKeys = viewModel.SortKey.Split(':');

            if (sortOrderKeys.Length != 2)
                return ExpressionBuilder.Generate<TModel>(viewModel.SortKey);

            viewModel.SortKey = sortOrderKeys[0];
            orderBy = (OrderBy)Enum.Parse(typeof(OrderBy), sortOrderKeys[1]);

            return ExpressionBuilder.Generate<TModel>(viewModel.SortKey);
        }
    }
}