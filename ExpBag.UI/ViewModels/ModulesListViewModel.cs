using ExpBag.Domain;
using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ExpBag.UI.ViewModels
{
    public class ModulesListViewModel: ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly ApplicationStore Store;
        
        public ObservableCollection<ModuleInfo> Modules { get; set; }


        public ReactiveCommand<Unit, Unit> NewModuleCommand;

        public ModulesListViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();

            Modules = new ObservableCollection<ModuleInfo>
            {
                new ModuleInfo
                {
                    ModuleName = "card-view"
                },
                new ModuleInfo
                {
                    ModuleName = "navbar-view"
                },
                
            };


            NewModuleCommand = ReactiveCommand.CreateFromTask(NewModuleCommandClick);
        }

        public async Task NewModuleCommandClick()
        {

        }
    }
}
