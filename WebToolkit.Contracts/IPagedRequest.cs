namespace WebToolkit.Contracts
{
    public interface IPagedRequest
    {
        int PageIndex { get; set; }
        int ItemsPerPage { get; set; }
    }
}