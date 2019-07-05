using Microsoft.Extensions.DependencyInjection;

namespace WebToolkit.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterServices(IServiceCollection services);
    }
}