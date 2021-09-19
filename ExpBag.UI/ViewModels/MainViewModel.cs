using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.Application.Constans;
using System.ComponentModel;
using ExpBag.Domain;
using System.Reactive;
using System.Diagnostics;
using System.Collections.ObjectModel;
using ExpBag.Infrastructure.Extentions;
using ExpBag.UI.Abstractions;
using ExpBag.Domain.DTO;

namespace ExpBag.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;

        private readonly ApplicationStore Store;
        private readonly IModuleService ModuleService;


        private ViewModelBase _left;
        public ViewModelBase Left
        {
            get => _left;
            set => this.RaiseAndSetIfChanged(ref _left, value);
        }

        private ViewModelBase _right;
        public ViewModelBase Right
        {
            get => _right;
            set => this.RaiseAndSetIfChanged(ref _right, value);
        }

        public ObservableCollection<ExpModuleDTO> UserModules { get; set; } = new ObservableCollection<ExpModuleDTO>();

        public MainViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();
            ModuleService = ServiceProvider.GetService<IModuleService>();

            Store.ViewSetupUpdated += UpdateViewSetup;
            UpdateViewSetup(Store.ViewSetup);

            Task.Run(Load);
        }

        private async Task Load()
        {
            var modules = await ModuleService.GetExpModulesAsync();
            foreach(var module in modules)
            {
                UserModules.Add(module);
            }
        }


        private void UpdateViewSetup(ViewSetups viewSetup)
        {
            switch(viewSetup)
            {
                case ViewSetups.EmptyProjectList:
                    Left = ServiceProvider.GetService<SidebarViewModel>();

                    break;
                case ViewSetups.DetailedProjectList:
                    Left = ServiceProvider.GetService<SidebarViewModel>();
                    Right = ServiceProvider.GetService<ProjectDetailsViewModel>();

                    (Right as ProjectDetailsViewModel).SelectedProject = (Left as SidebarViewModel).SelectedProject;
                    break;
                case ViewSetups.NewModuleSelect:
                    Left = ServiceProvider.GetService<SidebarViewModel>();
                    Right = ServiceProvider.GetService<NewModuleSelectViewModel>();

                    (Right as NewModuleSelectViewModel).Files = (Left as SidebarViewModel).SelectedProject.Components.ToObservableCollection();
                    break;
                case ViewSetups.NewModuleOptions:
                    Left = ServiceProvider.GetService<SidebarViewModel>();
                    Right = ServiceProvider.GetService<NewModuleOptionsViewModel>();
                    break;
                case ViewSetups.AddModule:
                    Left = ServiceProvider.GetService<SidebarViewModel>();
                    Right = ServiceProvider.GetService<AddModuleViewModel>();

                    (Right as AddModuleViewModel).Modules = UserModules;
                    (Right as AddModuleViewModel).Project = (Left as SidebarViewModel).SelectedProject;
                    break;
                default:
                    Left = null;
                    Right = null;
                    break;
            }
        }
    }
}
