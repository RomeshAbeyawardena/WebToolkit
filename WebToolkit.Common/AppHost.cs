using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    public class AppHost : IAppHost
    {
        private readonly IServiceCollection _serviceCollection;

        public IServiceProvider ServiceProvider => _serviceCollection.BuildServiceProvider();
        public ServiceProviderOptions ServiceProviderOptions { get; }
        public Type StartupType { get; }
        public IAppHost RegisterServices(Action<IServiceCollection> services)
        {
            services(_serviceCollection);
            return this;
        }

        public object GetStartupService()
        {
            return ServiceProvider.GetService(StartupType);
        }

        public static IAppHost CreateAppHost(Type startupType, Action<ServiceProviderOptions> serviceProviderOptions = null)
        {
            return new AppHost(startupType, serviceProviderOptions);
        }

        public static IAppHost<TStartup> CreateAppHost<TStartup>(Action<ServiceProviderOptions> serviceProviderOptions = null)
        {
            return AppHost<TStartup>.CreateAppHost(serviceProviderOptions);
        }

        protected AppHost(Type startupType, Action<ServiceProviderOptions> serviceProviderOptions, string appSettingFile = "appsettings.json")
        {
            StartupType = startupType;
            ServiceProviderOptions = new ServiceProviderOptions();
             serviceProviderOptions(ServiceProviderOptions);
            _serviceCollection = new ServiceCollection();
            _serviceCollection
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddJsonFile(appSettingFile)
                    .Build())
                .AddSingleton(startupType);
        }
    }

    public class AppHost<TStartup> : AppHost, IAppHost<TStartup>
    {
        protected AppHost(Action<ServiceProviderOptions> serviceProviderOptions) 
            : base(typeof(TStartup), serviceProviderOptions)
        {

        }

        public static IAppHost<TStartup> CreateAppHost(Action<ServiceProviderOptions> serviceProviderOptions = null)
        {
            return new AppHost<TStartup>(serviceProviderOptions);
        }

        public async Task<IAppHost<TStartup>> StartAsync(string[] args, Func<TStartup, string[], Task> startupAction)
        {
            await startupAction((TStartup) GetStartupService(), args);
            return this;
        }

        public new IAppHost<TStartup> RegisterServices(Action<IServiceCollection> services)
        {
            base.RegisterServices(services);
            return this;
        }

        public IAppHost<TStartup> Start(string[] args, Action<TStartup, string[]> startupAction)
        {
            startupAction((TStartup)GetStartupService(), args);
            return this;
        }
    }
}