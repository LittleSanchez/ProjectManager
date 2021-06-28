using Avalonia.Controls;
using ProjectManager.Domain.Models;
using ProjectManager.Loader.Abstractions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {

        //DI
        public IProjectSelector ProjectSelector { get; set; }
        public IProjectLoader ProjectLoader { get; set; }


        //Properties
        private List<ProjectComponent> files;
        public List<ProjectComponent> Files { 
            get => files; 
            set => this.RaiseAndSetIfChanged(ref files, value);  
        }

        //Commands

        public ReactiveCommand<Window, Unit> OpenProjectCommand{ get; }

        public MainWindowViewModel(
            IProjectSelector projectSelector,
            IProjectLoader projectLoader)
        {
            
            ProjectSelector = projectSelector;
            ProjectLoader = projectLoader;

            OpenProjectCommand = ReactiveCommand.CreateFromTask<Window>(OpenProject);
        }

        public async Task OpenProject(Window window)
        {
            var ofd = new OpenFileDialog();
            var project = this.ProjectSelector.OpenProject((await ofd.ShowAsync(window)).First());
            var projectInfo = this.ProjectLoader.Load(project);
            if (projectInfo != null)
            {
                this.Files = projectInfo.Components;
            }
        }

    }
}
