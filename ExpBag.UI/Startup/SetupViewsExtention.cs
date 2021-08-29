using Avalonia;
using ExpBag.Application.Interfaces;
using ExpBag.UI.Abstractions;
using ExpBag.UI.Store;
using ExpBag.UI.ViewModels;
using ExpBag.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.Startup
{
    public static class SetupViewsExtention
    {
        public static AppBuilder SetupViews(this AppBuilder appBuilder)
        {
            var serviceProvider = ServiceProviderFactory.ServiceProvider;
            var store = serviceProvider.GetService<ApplicationStore>();
            var viewService = serviceProvider.GetService<IAppViewService>();

            //Default startup view
            viewService.Show(Application.Constans.ViewSetups.EmptyProjectList);
            if (store.Profile == null)
                viewService.Show(Application.Constans.ViewSetups.Auth);
            return appBuilder;
        }
    }
}
