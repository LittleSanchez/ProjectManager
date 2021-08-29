using ExpBag.Application.Interfaces;
using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.UI.ViewModels;
using ExpBag.Application.Constans;
using ExpBag.UI.Abstractions;

namespace ExpBag.UI.Services
{
    public class AppViewService : IAppViewService
    {
        private readonly IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly ApplicationStore store;

        private readonly IDictionary<ViewSetups, Type> ViewSetups;

        public AppViewService()
        {
            store = serviceProvider.GetService<ApplicationStore>() ?? throw new NullReferenceException("ApplicationStore hasn't found in ServiceProvider");

            ViewSetups = new Dictionary<ViewSetups, Type>
            {
                {Application.Constans.ViewSetups.Auth, typeof(AuthViewModel)},
                {Application.Constans.ViewSetups.DetailedProjectList, typeof(ProjectListViewModel)}, //fwefwefwef
                {Application.Constans.ViewSetups.EmptyProjectList, typeof(ProjectListViewModel)},
            };
        }

        public void Show(Application.Constans.ViewSetups viewSetup)
        {
            //store.ActiveView = CastToField(store.ActiveView, serviceProvider.GetService(ViewSetups[viewSetup]));
            store.ViewSetup = viewSetup;
        }

        private T CastToField<T>(T obj, object? value) where T : class
        {
            return value as T;
        }
    }
}
