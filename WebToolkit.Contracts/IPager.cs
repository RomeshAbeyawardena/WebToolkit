using System;
using System.Collections.Generic;
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
    }
}