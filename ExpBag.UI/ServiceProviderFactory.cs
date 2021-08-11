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

namespace ExpBag.UI
{
    public class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderFactory()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ITempController, TempContoller>();

            serviceCollection.AddSingleton<Loader.Abstractions.IProjectLoader, NpmProjectLoader>();
            serviceCollection.AddSingleton<IProjectSelector, ProjectSelector>();
            serviceCollection.AddSingleton<IProjectSerializer, ProjectSerializer>();
            serviceCollection.AddSingleton<IProjectModuleCompiler, NpmModuleCompiler>();

            serviceCollection.AddSingleton<ITempController, TempContoller>();


            ServiceProvider = serviceCollection.BuildServiceProvider();

        }
    }
}
