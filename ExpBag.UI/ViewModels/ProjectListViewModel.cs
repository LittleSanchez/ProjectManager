using Avalonia.Controls;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using ExpBag.Loader;
using ExpBag.Loader.Abstractions;
using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ExpBag.UI.ViewModels
{
    public class ProjectListViewModel : ViewModelBase
    {
        public readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;

        //DI
        public IProjectSelector? ProjectSelector { get; set; }
        public IProjectLoader? ProjectLoader { get; set; }
        //public IProjectSerializer ProjectSerializer { get; set; }
        public IProjectModuleCompiler? ProjectModuleCompiler { get; set; }
        public ITempController? TempController { get; set; }
        public IAuthService? AuthService { get; set; }
        public ApplicationStore? ApplicationStore{ get; set; }

        //Store

        public UserDTO Profile { get; set; }
        private void UpdateProfile(UserDTO profile) => Profile = profile;


        //Properties
        private List<ExpModuleFileDTO> files;
        public List<ExpModuleFileDTO> Files
        {
            get => files;
            set => this.RaiseAndSetIfChanged(ref files, value);
        }

        private ExpModuleFileDTO selectedFile;
        public ExpModuleFileDTO SelectedFile
        {
            get => selectedFile;
            set => this.RaiseAndSetIfChanged(ref selectedFile, value);
        }

        private ProjectInfo selectedProject;
        public ProjectInfo SelectedProject
        {
            get => selectedProject;
            set => this.RaiseAndSetIfChanged(ref selectedProject, value);
        }


        //Commands

        public ReactiveCommand<Window, Unit> OpenProjectCommand { get; }
        public ReactiveCommand<Unit, Unit> SelectListItemCommand { get; }


        public ProjectListViewModel()
        {

            ProjectSelector = ServiceProvider.GetService<IProjectSelector>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();
            ProjectModuleCompiler = ServiceProvider.GetService<IProjectModuleCompiler>();
            TempController = ServiceProvider.GetService<ITempController>();
            AuthService = ServiceProvider.GetService<IAuthService>();
            ApplicationStore = ServiceProvider.GetService<ApplicationStore>();

            OpenProjectCommand = ReactiveCommand.CreateFromTask<Window>(OpenProject);
            SelectListItemCommand = ReactiveCommand.CreateFromTask(SelectListItem);

            //Connecting store events
            ApplicationStore!.ProfileUpdated += UpdateProfile;
        }

        public async Task OpenProject(Window window)
        {
            var ofd = new OpenFileDialog();
            var project = this.ProjectSelector.OpenProject((await ofd.ShowAsync(window)).First());
            var projectInfo = this.ProjectLoader.Load(project);
            if (projectInfo != null)
            {
                SelectedProject = projectInfo;
                //this.Files = projectInfo.Components;
            }
        }

        public async Task SelectListItem()
        {
            var module = await ProjectModuleCompiler.CompileAsync(
                SelectedProject,
                new NpmModuleCompilerOptions
                {
                    DestinationFolder = TempController.CreateTempDirectory(Path.GetFileName(SelectedFile.FilePath)),
                    TargetFile = SelectedFile.FilePath,
                    ModuleName = Path.GetFileNameWithoutExtension(SelectedFile.FileName)
                });
            
        }

    }
}
