using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebToolkit.Contracts
{
    public interface IAppHost
    {
        ServiceProviderOptions ServiceProviderOptions { get; }
        IServiceProvider ServiceProvider { get; }
        IAppHost RegisterServices(Action<IServiceCollection> services);
        object GetStartupService();
    }

    public interface IAppHost<TStartup> : IAppHost
    {
        Task<IAppHost<TStartup>> StartAsync(string[] args, Func<TStartup, string[], Task> startupAction);
        IAppHost<TStartup> Start(string[] args, Action<TStartup, string[]> startupAction);
        new IAppHost<TStartup> RegisterServices(Action<IServiceCollection> services);
    }
}