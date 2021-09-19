using ExpBag.Domain;
using ExpBag.UI.Startup;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.UI.Abstractions;
using ExpBag.Domain.DTO;
using ExpBag.Infrastructure.Extentions;
using ExpBag.Loader.Abstractions;

namespace ExpBag.UI.ViewModels
{
    public class ProjectDetailsViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly IModuleService ModuleService;
        private readonly IProjectLoader ProjectLoader;

        private ProjectInfo? _projectInfo;

        public ProjectInfo? SelectedProject
        {
            get { return _projectInfo; }
            set
            {
                this.RaiseAndSetIfChanged(ref _projectInfo, value);
                LoadProjectModules();
            }
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
            ModuleService = ServiceProvider.GetService<IModuleService>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();


        }

        private async Task LoadProjectModules()
        {
            //var modules = await ModuleService.GetExpModulesAsync();
            //SelectedProject.ExpModules.Clear();
            //ModulesList.Modules.Clear();
            //foreach (var module in modules)
            //{
            //    SelectedProject.ExpModules.Add(module as ExpModuleStored);
            //    ModulesList.Modules.Add(module);
            //}
            //if (SelectedProject.ExpModules == null || SelectedProject.ExpModules.Count == 0)
            //{
            //    ProjectLoader.GetStoredModules(SelectedProject);
            //}
            ModulesList.Project = SelectedProject;
        }

    }
}
