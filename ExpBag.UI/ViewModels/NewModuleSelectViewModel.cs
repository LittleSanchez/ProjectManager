using ExpBag.Domain.DTO;
using ExpBag.UI.Abstractions;
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

namespace ExpBag.UI.ViewModels
{
    public class NewModuleSelectViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly IAppViewService ViewService;


        private ExpModuleFileDTO _selectedFile;
        public ExpModuleFileDTO SelectedFile
        {
            get { return _selectedFile; }
            set { this.RaiseAndSetIfChanged(ref _selectedFile, value); }
        }

        private ObservableCollection<ExpModuleFileDTO> _files;
        public ObservableCollection<ExpModuleFileDTO> Files
        {
            get { return _files; }
            set { this.RaiseAndSetIfChanged(ref _files, value); }
        }

        public ReactiveCommand<Unit, Unit> SelectCommand { get; set; }


        public NewModuleSelectViewModel()
        {
            ViewService = ServiceProvider.GetService<IAppViewService>();

            Files = new ObservableCollection<ExpModuleFileDTO>();

            SelectCommand = ReactiveCommand.CreateFromTask(SelectCommandTask);
        }

        public async Task SelectCommandTask()
        {
            ServiceProvider.GetService<NewModuleOptionsViewModel>().ModuleFile = SelectedFile;
            ViewService.Show(Application.Constans.ViewSetups.NewModuleOptions);
        }
    }
}
