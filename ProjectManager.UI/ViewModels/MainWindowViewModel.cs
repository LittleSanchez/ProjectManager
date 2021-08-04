using Avalonia.Controls;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain;
using ProjectManager.Domain.Models;
using ProjectManager.Loader.Abstractions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;



        private ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MainWindowViewModel()
        {
            Content = new ProjectListViewModel(
                serviceProvider.GetService(typeof(IProjectSelector)) as IProjectSelector,
                        serviceProvider.GetService(typeof(IProjectLoader)) as IProjectLoader,
                        serviceProvider.GetService(typeof(IProjectSerializer)) as IProjectSerializer,
                        serviceProvider.GetService(typeof(IProjectModuleCompiler)) as IProjectModuleCompiler
                );

            //Content = new AuthViewModel();
        }
    }
}
