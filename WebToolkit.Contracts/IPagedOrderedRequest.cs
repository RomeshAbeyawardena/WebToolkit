namespace WebToolkit.Contracts
{
    public interface IPagedOrderedRequest : IPagedRequest
    {
        string SortKey { get; set; }
    }
}