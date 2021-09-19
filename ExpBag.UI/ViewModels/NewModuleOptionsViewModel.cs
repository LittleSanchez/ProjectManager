using ExpBag.Domain.DTO;
using ExpBag.UI.Abstractions;
using ExpBag.UI.Startup;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.Loader.Abstractions;
using System.IO;
using ExpBag.Application.Interfaces;
using ExpBag.Loader.Models;
using Newtonsoft.Json;
using ExpBag.Domain.ModuleInfoTypes.Npm;
using ExpBag.Domain;

namespace ExpBag.UI.ViewModels
{
    public class NewModuleOptionsViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly IAppViewService ViewService;
        private readonly IProjectLoader ProjectLoader;
        private readonly IProjectModuleCompiler ProjectModuleCompiler;
        private readonly IProjectSelector ProjectSelector;
        private readonly ITempController TempController;
        private readonly IModuleService ModuleService;


        private ExpModuleFileDTO _moduleFile;

        public ExpModuleFileDTO ModuleFile
        {
            get { return _moduleFile; }
            set { this.RaiseAndSetIfChanged(ref _moduleFile, value); }
        }

        private string _moduleName;

        public string ModuleName
        {
            get { return _moduleName; }
            set { this.RaiseAndSetIfChanged(ref _moduleName, value); }
        }

        private string _moduleVersion;

        public string ModuleVersion
        {
            get { return _moduleVersion; }
            set { this.RaiseAndSetIfChanged(ref _moduleVersion, value); }
        }



        public ReactiveCommand<Unit, Unit> CancelCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PreviewCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CreateCommand { get; set; }

        public NewModuleOptionsViewModel()
        {
            ViewService = ServiceProvider.GetService<IAppViewService>();
            ProjectLoader = ServiceProvider.GetService<IProjectLoader>();
            ProjectModuleCompiler = ServiceProvider.GetService<IProjectModuleCompiler>();
            ProjectSelector = ServiceProvider.GetService<IProjectSelector>();
            TempController = ServiceProvider.GetService<ITempController>();
            ModuleService = ServiceProvider.GetService<IModuleService>();

            //ModuleFile = this.GetParam<ExpModuleFileDTO>("NewModuleFile");
            CancelCommand = ReactiveCommand.CreateFromTask(CancelCommandTask);
            PreviewCommand = ReactiveCommand.CreateFromTask(PreviewCommandTask);
            CreateCommand = ReactiveCommand.CreateFromTask(CreateCommandTask);
        }

        public async Task CancelCommandTask()
        {
            ViewService.Show(Application.Constans.ViewSetups.DetailedProjectList);
        }
        public async Task PreviewCommandTask()
        {
            ViewService.Show(Application.Constans.ViewSetups.NewModuleSelect);
        }
        public async Task CreateCommandTask()
        {
            var project = ServiceProvider.GetService<ProjectDetailsViewModel>().SelectedProject;
            var compilerOptions = new NpmModuleCompilerOptions
            {
                DestinationFolder = TempController.CreateTempDirectory(Path.GetFileName(ModuleFile.FilePath)),
                TargetFile = ModuleFile.FilePath,
                ModuleName = ModuleName
            };
            var moduleInfo = await ProjectModuleCompiler.CompileAsync(
                project,
                compilerOptions);
            var module = new ExpModuleDTO
            {
                ModuleInfo = JsonConvert.SerializeObject(moduleInfo),
                ModuleInfoType = "npm",
                ModuleName = moduleInfo.ModuleName,
                IncludedFiles = (moduleInfo as NpmModuleInfo).IncludedFiles
                    .Select(x => new ExpModuleFileDTO
                    {
                        ModuleName = moduleInfo.ModuleName,
                        FileName = Path.GetFileName(x),
                        FilePath = x,
                        IsLoaded = false
                    }).ToList()
            };
            module = await ModuleService.AddExpModuleAsync(module);
            module = await ModuleService.UploadFiles(module, compilerOptions.DestinationFolder);
            project.ExpModules.Add(JsonConvert.DeserializeObject<ExpModuleStored>(JsonConvert.SerializeObject(module)));
            ViewService.Show(Application.Constans.ViewSetups.DetailedProjectList);
        }

    }
}
