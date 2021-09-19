using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpBag.Domain;
using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Controls;
using System.Reactive;
using ExpBag.Loader.Abstractions;
using System.Collections.ObjectModel;
using ExpBag.Application.Constans;
using ExpBag.UI.Abstractions;

namespace ExpBag.UI.ViewModels
{
    public class SidebarViewModel : ViewModelBase
    {
        private IServiceProvider ServiceProvider { get; } = ServiceProviderFactory.ServiceProvider;
        public IProjectSelector? ProjectSelector { get; set; }
        public IProjectLoader? ProjectLoader { get; set; }
        public IModuleService? ModuleService { get; set; }
        private ApplicationStore? Store { get; }

        //Binding properties

        private ObservableCollection<ProjectInfo>? _projects;
        public ObservableCollection<ProjectInfo>? Projects
        {
            get { return _projects; }
            set { this.RaiseAndSetIfChanged(ref _projects, value); }
        }

        private ProjectInfo? _selectedProject;

        public ProjectInfo? SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedProject, value);
                if (value == null)
                    Store.ViewSetup = ViewSetups.EmptyProjectList;
                else
                    Store.ViewSetup = ViewSetups.DetailedProjectList;
            }
        }

        //Reactive commands
        public ReactiveCommand<Window, Unit> OpenProjectCommand { get; set; }

        public SidebarViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();
            ProjectSelector = ServiceProvider.GetService<IProjectSelector>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();
            ModuleService = ServiceProvider.GetService<IModuleService>();

            Store.ProjectsUpdated += UpdateProjects;
            UpdateProjects(Store?.Projects);

            OpenProjectCommand = ReactiveCommand.CreateFromTask<Window>(OpenProject);
        }


        private void UpdateProjects(List<ProjectInfo>? projects)
        {
            if (Projects == null)
                Projects = new ObservableCollection<ProjectInfo>();
            Projects!.Clear();
            foreach (var item in projects)
            {
                Projects.Add(item);
            }
        }

        public async Task OpenProject(Window window)
        {
            var ofd = new OpenFileDialog();
            var project = this.ProjectSelector.OpenProject((await ofd.ShowAsync(window)).First());
            var projectInfo = await this.ProjectLoader.LoadProjectAsync(project);
            if (projectInfo != null)
            {
                Projects.Add(projectInfo);
                Store.Projects = Projects.ToList();
                SelectedProject = projectInfo;
            }
        }


    }
}
