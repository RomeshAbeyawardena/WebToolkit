using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebToolkit.Common.Extensions;
using WebToolkit.Contracts;

namespace WebToolkit.Common
{
    /// <summary>
    /// Represents a simple AppHost to host console applications with dependency injection support
    /// </summary>
    public class AppHost : IAppHost
    {
        protected readonly IServiceCollection ServiceCollection;

        public IServiceProvider ServiceProvider => ServiceCollection.BuildServiceProvider();
        public ServiceProviderOptions ServiceProviderOptions { get; }
        public Type StartupType { get; }
        public IAppHost RegisterServices(Action<IServiceCollection> services)
        {
            services(ServiceCollection);
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
            ServiceCollection = new ServiceCollection();
            ServiceCollection
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
        
        /// <summary>
        /// Creates an App Host
        /// </summary>
        /// <param name="serviceProviderOptions"></param>
        /// <returns></returns>
        public static IAppHost<TStartup> CreateAppHost(Action<ServiceProviderOptions> serviceProviderOptions = null)
        {
            return new AppHost<TStartup>(serviceProviderOptions);
        }

        //Invokes an async Task implemented within TStartup that represents a start of the application
        public async Task<IAppHost<TStartup>> StartAsync(string[] args, Func<TStartup, string[], Task> startupAction)
        {
            await startupAction((TStartup) GetStartupService(), args);
            return this;
        }
        
        //Registers services within this instance containers
        public new IAppHost<TStartup> RegisterServices(Action<IServiceCollection> services)
        {
            base.RegisterServices(services);
            return this;
        }
        
        //Invokes a method implemented within TStartup that represents service registration
        public IAppHost<TStartup> RegisterServices(Func<TStartup, IServiceCollection> services)
        {
            services(StartupService)
                .ForEach(s => ServiceCollection.Add(s));

            return this;
        }

        //Invokes a method implemented within TStartup that represents a start of the application
        public IAppHost<TStartup> Start(string[] args, Action<TStartup, string[]> startupAction)
        {
            startupAction(StartupService, args);
            return this;
        }

        private TStartup StartupService => (TStartup) GetStartupService();
    }
}