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
            var skippedNumber = itemsPerPage * pageIndex;
            var totalInQuery = await query.CountAsync();
            if (skippedNumber > totalInQuery)
                return query;

            var skippedQuery = query.Skip(skippedNumber);
            
            return await skippedQuery.CountAsync() < itemsPerPage 
                ? skippedQuery 
                : skippedQuery.Take(itemsPerPage);
        }

        private TPagedResult CreatePagedResult<TPagedResult>(int currentPageIndex, int itemsPerPage, int totalItems, IEnumerable<TModel> pagedResults)
            where TPagedResult : IPagedResult<TModel>
        {
            var pagedResult = Activator.CreateInstance<TPagedResult>();
            pagedResult.CurrentPageIndex = currentPageIndex;
            pagedResult.TotalPages = totalItems == 0 ? 0 : (int)Math.Ceiling( (decimal)totalItems / itemsPerPage );
            pagedResult.TotalItems = totalItems;
            pagedResult.Results = pagedResults;

            return pagedResult;
        }

        public Pager(IRepository<TModel> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<TPagedResult> GetPage<TPagedResult, TKey>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null,
            OrderBy orderBy = OrderBy.None, Expression<Func<TModel, TKey>> orderByExpression = null) 
            where TPagedResult : IPagedResult<TModel>
        {
            var query = _modelRepository.Query(filterExpression);
            var totalItems = await query.CountAsync();
            if (orderBy == OrderBy.None)
                return CreatePagedResult<TPagedResult>(pagedRequest.PageIndex, pagedRequest.ItemsPerPage, totalItems, 
                    await (await GetPagedResult(query, pagedRequest.PageIndex, pagedRequest.ItemsPerPage))
                    .ToArrayAsync());

            if(orderByExpression == null)
                throw new ArgumentNullException(nameof(orderByExpression));

            return CreatePagedResult<TPagedResult>(pagedRequest.PageIndex, pagedRequest.ItemsPerPage, totalItems, 
                await (await GetPagedResult(orderBy == OrderBy.Ascending
                ? query.OrderBy(orderByExpression)
                : query.OrderByDescending(orderByExpression), pagedRequest.PageIndex, pagedRequest.ItemsPerPage))
                .ToArrayAsync());
        }

        public Task<TPagedResult> GetPage<TPagedResult>(IPagedRequest pagedRequest, Expression<Func<TModel, bool>> filterExpression = null)
            where TPagedResult : IPagedResult<TModel>
        {
            return GetPage<TPagedResult, object>(pagedRequest, filterExpression);
        }
    }
}