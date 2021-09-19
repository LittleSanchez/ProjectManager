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
using ExpBag.UI.Abstractions;
using ExpBag.Domain.DTO;
using ExpBag.Loader.Abstractions;

namespace ExpBag.UI.ViewModels
{
    public class ModulesListViewModel: ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly ApplicationStore Store;
        private readonly IAppViewService ViewService;
        private readonly IProjectLoader ProjectLoader;

        //public ObservableCollection<ExpModuleDTO> Modules { get; set; } = new ObservableCollection<ExpModuleDTO>();
        private ProjectInfo _project;

        public ProjectInfo Project
        {
            get { return _project; }
            set { this.RaiseAndSetIfChanged(ref _project, value); }
        }

        public ReactiveCommand<Unit, Unit> NewModuleCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ReloadModulesCommand { get; set; }
        public ReactiveCommand<Unit, Unit> AddModuleCommand { get; set; }
        public ReactiveCommand<ExpModuleStored, Unit> ExcludeModuleCommand { get; set; }

        public ModulesListViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();

            ViewService = ServiceProvider.GetService<IAppViewService>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();

            NewModuleCommand = ReactiveCommand.CreateFromTask(NewModuleCommandClick);
            ReloadModulesCommand = ReactiveCommand.CreateFromTask(ReloadModulesCommandClick);
            AddModuleCommand = ReactiveCommand.CreateFromTask(AddModuleCommandClick);
            ExcludeModuleCommand = ReactiveCommand.CreateFromTask<ExpModuleStored>(ExcludeModuleCommandTask);

        }

        public async Task NewModuleCommandClick()
        {
            ViewService.Show(Application.Constans.ViewSetups.NewModuleSelect);
        }

        public async Task ReloadModulesCommandClick()
        {
            ProjectLoader.SaveStoredModules(Project);
            await ProjectLoader.LoadModuleAsync(Project);
        }

        public async Task AddModuleCommandClick()
        {
            Store.ViewSetup = Application.Constans.ViewSetups.AddModule;
        }

        public async Task ExcludeModuleCommandTask(ExpModuleStored storedModule)
        {
            if (storedModule != null)
            {
                Project.ExpModules.Remove(storedModule);
            }
        }
    }
}
