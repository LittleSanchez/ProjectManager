using ExpBag.Domain;
using ExpBag.UI.Startup;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ExpBag.UI.ViewModels
{
    public class ProjectDetailsViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;

        private ProjectInfo? _projectInfo;

        public ProjectInfo? SelectedProject
        {
            get { return _projectInfo; }
            set { this.RaiseAndSetIfChanged(ref _projectInfo, value); }
        }

        private ModulesListViewModel _modulesList;

        public ModulesListViewModel ModulesList
        {
            get { return _modulesList; }
            set { this.RaiseAndSetIfChanged(ref _modulesList, value); }
        }


        public ProjectDetailsViewModel()
        {
            ModulesList = ServiceProvider.GetService<ModulesListViewModel>();
            //ModulesList.Modules = SelectedProject?.ExpModules;
        }

    }
}
