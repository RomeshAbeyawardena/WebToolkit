using System.Collections.Generic;

namespace WebToolkit.Contracts
{
    public interface IPagedResult<TModel>
    {
        IEnumerable<TModel> Results { get; set; }
        int TotalPages { get; set; }
        int TotalItems { get; set; }
        int CurrentPageIndex { get; set; }
    }
}