using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebToolkit.Contracts
{
    /// <summary>
    /// Represents a simple AppHost to host console applications with dependency injection support
    /// </summary>
    public interface IAppHost
    {
        ServiceProviderOptions ServiceProviderOptions { get; }
        IServiceProvider ServiceProvider { get; }
        IAppHost RegisterServices(Action<IServiceCollection> services);
        object GetStartupService();
    }

    /// <summary>
    /// Represents a simple AppHost to host console applications with dependency injection support
    /// </summary>
    /// <typeparam name="TStartup">Class containing startup logic</typeparam>
    public interface IAppHost<TStartup> : IAppHost
    {
        //Invokes an async Task implemented within TStartup that represents a start of the application
        Task<IAppHost<TStartup>> StartAsync(string[] args, Func<TStartup, string[], Task> startupAction);
        //Invokes a method implemented within TStartup that represents a start of the
        IAppHost<TStartup> Start(string[] args, Action<TStartup, string[]> startupAction);
        //Registers services within this instance containers
        new IAppHost<TStartup> RegisterServices(Action<IServiceCollection> services);
        //Invokes a method implemented within TStartup that represents service registration
        IAppHost<TStartup> RegisterServices(Func<TStartup, IServiceCollection> services);
    }
}