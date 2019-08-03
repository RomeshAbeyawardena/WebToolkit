using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebToolkit.Contracts;
using WebToolkit.Contracts.Data;
using WebToolkit.Shared;

namespace WebToolkit.Common
{
    public class Pager<TModel> : IPager<TModel> where TModel : class
    {
        private readonly IRepository<TModel> _modelRepository;

        private async Task<IQueryable<TModel>> GetPagedResult(IQueryable<TModel> query, int pageIndex, int itemsPerPage)
        {
            var skippedNumber = pageIndex == 0 ? 0 : (itemsPerPage * pageIndex) - 1;
            var totalInQuery = await query.CountAsync();
            if (skippedNumber > totalInQuery)
                return query;

            var skippedQuery = query.Skip(skippedNumber);
            
            return await skippedQuery.CountAsync() < itemsPerPage 
                ? skippedQuery 
                : skippedQuery.Take(itemsPerPage);
        }

        public Pager(IRepository<TModel> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<TModel>> GetPage<TKey>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null,
            OrderBy orderBy = OrderBy.None, Expression<Func<TModel, TKey>> orderByExpression = null)
        {
            var query = _modelRepository.Query(filterExpression);

            if (orderBy == OrderBy.None)
                return await (await GetPagedResult(query, pagedRequest.PageIndex, pagedRequest.ItemsPerPage))
                    .ToArrayAsync();

            if(orderByExpression == null)
                throw new ArgumentNullException(nameof(orderByExpression));

            return await (await GetPagedResult(orderBy == OrderBy.Ascending
                ? query.OrderBy(orderByExpression)
                : query.OrderByDescending(orderByExpression), pagedRequest.PageIndex, pagedRequest.ItemsPerPage))
                .ToArrayAsync();
        }

        public Task<IEnumerable<TModel>> GetPage(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null)
        {
            return GetPage<object>(pagedRequest, filterExpression);
        }
    }
}