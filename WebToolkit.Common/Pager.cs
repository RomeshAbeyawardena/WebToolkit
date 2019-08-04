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

        private PagerOptions<TModel, TKey> GetPagerOptions<TKey>(Action<PagerOptions<TModel, TKey>> pagerOptionsActions)
        {
            var pagerOptions = new PagerOptions<TModel, TKey>();
            pagerOptionsActions(pagerOptions);

            return pagerOptions;
        }

        public Pager(IRepository<TModel> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<TPagedResult> GetPage<TPagedResult, TKey>(IPagedRequest pagedRequest, Action<PagerOptions<TModel, TKey>> pagerOptions) 
            where TPagedResult : IPagedResult<TModel>
        {
            var options = GetPagerOptions(pagerOptions);

            var query = _modelRepository.Query(options.FilterExpression);
            var totalItems = await query.CountAsync();
            
            if(options.OrderByExpression == null)
                throw new ArgumentNullException(nameof(options.OrderByExpression));

            return CreatePagedResult<TPagedResult>(pagedRequest.PageIndex, pagedRequest.ItemsPerPage, totalItems, 
                await (await GetPagedResult(options.OrderBy == OrderBy.Ascending
                ? query.OrderBy(options.OrderByExpression)
                : query.OrderByDescending(options.OrderByExpression), pagedRequest.PageIndex, pagedRequest.ItemsPerPage))
                .ToArrayAsync());
        }

        public Task<TPagedResult> GetPage<TPagedResult>(IPagedRequest pagedRequest, Action<PagerOptions<TModel, object>> pagerOptions)
            where TPagedResult : IPagedResult<TModel>
        {
            return GetPage<TPagedResult, object>(pagedRequest, pagerOptions);
        }
    }
}