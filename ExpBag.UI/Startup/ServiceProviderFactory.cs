using Microsoft.Extensions.DependencyInjection;
using ExpBag.Application.Interfaces;
using ExpBag.Infrastructure.Environment;
using ExpBag.Loader;
using ExpBag.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpBag.Infrastructure.Network;
using Avalonia;
using ExpBag.UI.ViewModels;
using ExpBag.UI.Store;
using ExpBag.UI.Services;
using ExpBag.UI.Abstractions;

namespace ExpBag.UI.Startup
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<IAppViewService, AppViewService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<AuthViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ProjectListViewModel>();
            services.AddSingleton<SidebarViewModel>();
            services.AddSingleton<ProjectDetailsViewModel>();
            services.AddSingleton<ModulesListViewModel>();
            services.AddSingleton<NewModuleViewModel>();


        }

        public static AppBuilder ConfigureServices(this AppBuilder appBuilder)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IProjectLoader, NpmProjectLoader>();
            serviceCollection.AddSingleton<IProjectSelector, ProjectSelector>();
            serviceCollection.AddSingleton<IProjectModuleCompiler, NpmModuleCompiler>();

            serviceCollection.AddSingleton<ITempController, TempContoller>();
            serviceCollection.AddSingleton<IAuthService, AuthService>();

            serviceCollection.AddSingleton(StoreLoader.LoadStore());

            serviceCollection.AddSingleton<IAppAuthService, AppAuthService>();

            ConfigureViews(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            return appBuilder;
        }
    }
}
