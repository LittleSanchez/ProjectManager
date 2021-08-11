using Avalonia.Controls;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.ViewModels
{
    public class ProjectListViewModel : ViewModelBase
    {
        public readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;

        //DI
        public IProjectSelector ProjectSelector { get; set; }
        public IProjectLoader ProjectLoader { get; set; }
        public IProjectSerializer ProjectSerializer { get; set; }
        public IProjectModuleCompiler ProjectModuleCompiler { get; set; }
        public ITempController TempController { get; set; }


        //Properties
        private List<ProjectComponent> files;
        public List<ProjectComponent> Files
        {
            get => files;
            set => this.RaiseAndSetIfChanged(ref files, value);
        }

        private ProjectComponent selectedFile;
        public ProjectComponent SelectedFile
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


        public ProjectListViewModel(
            IProjectSelector projectSelector,
            IProjectLoader projectLoader,
            IProjectSerializer projectSerializer,
            IProjectModuleCompiler projectModuleCompiler, 
            ITempController tempController)
        {

            ProjectSelector = projectSelector;
            ProjectLoader = projectLoader;
            ProjectSerializer = projectSerializer;
            ProjectModuleCompiler = projectModuleCompiler;
            TempController = tempController;

            OpenProjectCommand = ReactiveCommand.CreateFromTask<Window>(OpenProject);
            SelectListItemCommand = ReactiveCommand.CreateFromTask(SelectListItem);
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
            Debug.WriteLine(SelectedFile);

            var module = await ProjectModuleCompiler.CompileAsync(
                SelectedProject, 
                SelectedFile.FilePath, 
                TempController.CreateTempDirectory(Path.GetFileName(SelectedFile.FilePath)));
            //ProjectSerializer.Serialize(filePath, module);

        }

    }
}
