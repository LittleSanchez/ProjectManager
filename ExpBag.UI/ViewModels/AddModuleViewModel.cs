using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.UI.Startup;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.UI.Store;
using Newtonsoft.Json;
using ExpBag.UI.Abstractions;
using ExpBag.Loader.Abstractions;

namespace ExpBag.UI.ViewModels
{
    public class AddModuleViewModel:ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly ApplicationStore Store;
        private readonly IModuleService ModuleService;
        private readonly IProjectLoader ProjectLoader;

        private ObservableCollection<ExpModuleDTO> _modules = new ObservableCollection<ExpModuleDTO>();
        public ObservableCollection<ExpModuleDTO> Modules
        {
            get { return _modules; }
            set { this.RaiseAndSetIfChanged(ref _modules, value); }
        } 

        private ProjectInfo _project;

        public ProjectInfo Project
        {
            get { return _project; }
            set { this.RaiseAndSetIfChanged(ref _project, value); }
        }

        private ExpModuleDTO _selectedModule;

        public ExpModuleDTO SelectedModule
        {
            get { return _selectedModule; }
            set { this.RaiseAndSetIfChanged(ref _selectedModule, value); }
        }


        public ReactiveCommand<Unit,Unit> CancelCommand { get; set; }
        public ReactiveCommand<Unit,Unit> AddModuleCommand { get; set; }

        public AddModuleViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();
            ModuleService = ServiceProvider.GetService<IModuleService>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();

            CancelCommand = ReactiveCommand.CreateFromTask(CancelCommandTask);
            AddModuleCommand = ReactiveCommand.CreateFromTask(AddModuleCommandTask);
            Task.Run(async () =>
            {
                Modules.Clear();
                Store.UserModules = await ModuleService.GetExpModulesAsync();
                
            });
        }

        public async Task AddModuleCommandTask()
        {
            var moduleStored = JsonConvert.DeserializeObject<ExpModuleStored>(JsonConvert.SerializeObject(SelectedModule));
            Project.ExpModules.Add(moduleStored);
            ProjectLoader.SaveStoredModules(Project);
            ServiceProvider.GetService<ModulesListViewModel>().Project = Project;
            Store.ViewSetup = Application.Constans.ViewSetups.DetailedProjectList;
        }

        public async Task CancelCommandTask()
        {
            Store.ViewSetup = Application.Constans.ViewSetups.DetailedProjectList;
        }


        public void UserModulesUpdated(ObservableCollection<ExpModuleDTO> modules)
        {
            Modules = modules;
        }

    }
}
