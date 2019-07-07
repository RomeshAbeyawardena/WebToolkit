namespace WebToolkit.Contracts
{
    public interface IIdentity<TIdentity>
    {
        TIdentity Id { get; set; }
    }

    public interface IIdentity : IIdentity<int>
    {

    }
}