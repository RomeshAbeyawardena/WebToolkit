﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebToolkit.Contracts
{
    public interface IAppBuilder<out TStart> where TStart : class
    {
        IAppBuilder<TStart> RegisterServicesFromAssemblies<TServiceBroker>() where TServiceBroker : IServiceBroker;
        IAppBuilder<TStart> ConfigureEntryPoints(Func<TStart, int> entryPoint, Func<TStart, Task<int>> entryPointAsync = null);
        TService GetRequiredService<TService>();
        IServiceProvider ServiceProvider { get; }
        IServiceCollection Services { get; }
        IAppBuilder<TStart> RegisterServices(Action<IServiceCollection> serviceRegistration = null, 
            IConfigurationBuilder configurationBuilder = null);
        int Start(bool throwOnError = true);
        Task<int> StartAsync(bool throwOnError = true);
    }
}