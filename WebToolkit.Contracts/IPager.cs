﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebToolkit.Shared;

namespace WebToolkit.Contracts
{
    public interface IPager<TModel>
    {
        Task<TPagedResult> GetPage<TPagedResult, TKey>(IPagedRequest pagedRequest, Action<PagerOptions<TModel, TKey>> pagerOptions) 
            where TPagedResult : IPagedResult<TModel>;
        
        Task<TPagedResult> GetPage<TPagedResult>(IPagedRequest pagedRequest, Action<PagerOptions<TModel, object>> pagerOptions)
            where TPagedResult : IPagedResult<TModel>;

        [Obsolete("Use improved GetPage method with pagerOptions parameter")]
        Task<TPagedResult> GetPage<TPagedResult, TKey>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null,
            OrderBy orderBy = OrderBy.None, Expression<Func<TModel, TKey>> orderByExpression = null) where TPagedResult : IPagedResult<TModel>;
        
        [Obsolete("Use improved GetPage method with pagerOptions parameter")]
        Task<TPagedResult> GetPage<TPagedResult>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null)
            where TPagedResult : IPagedResult<TModel>;
    }
}