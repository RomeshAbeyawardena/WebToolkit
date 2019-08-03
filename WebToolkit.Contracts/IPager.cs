using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebToolkit.Shared;

namespace WebToolkit.Contracts
{
    public interface IPager<TModel>
    {
        Task<IEnumerable<TModel>> GetPage<TKey>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null,
            OrderBy orderBy = OrderBy.None, Expression<Func<TModel, TKey>> orderByExpression = null);
        
        Task<IEnumerable<TModel>> GetPage(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null);
    }
}