namespace WebToolkit.Contracts
{
    public interface IDefaultValuesFactory
    {
        void Assign<TModel>(TModel model);
    }
}