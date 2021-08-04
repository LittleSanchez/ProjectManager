using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Interfaces;
using ProjectManager.Infrastructure.Environment;
using ProjectManager.Loader;
using ProjectManager.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.UI
{
    public class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderFactory()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ITempController, TempContoller>();

            serviceCollection.AddSingleton<IProjectLoader, ProjectLoader>();
            serviceCollection.AddSingleton<IProjectSelector, ProjectSelector>();
            serviceCollection.AddSingleton<IProjectSerializer, ProjectSerializer>();
            serviceCollection.AddSingleton<IProjectModuleCompiler, ProjectModuleCompiler>();


            ServiceProvider = serviceCollection.BuildServiceProvider();

        }
    }
}
