namespace WebToolkit.Contracts.Factories
{
    public interface IPagerFactory
    {
        IPager<TModel> GetPager<TModel>();
    }
}