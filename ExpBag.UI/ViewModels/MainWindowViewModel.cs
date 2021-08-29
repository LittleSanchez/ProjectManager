using Avalonia.Controls;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ExpBag.Infrastructure.Extentions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.Application.Constans;

namespace ExpBag.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly ApplicationStore? Store;


        private ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { this.RaiseAndSetIfChanged(ref _windowWidth, value); }
        }

        private double _windowHeight;

        public double WindowHeight
        {
            get { return _windowHeight; }
            set { this.RaiseAndSetIfChanged(ref _windowHeight, value); }
        }





        public MainWindowViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();

            //Content = applicationStore!.ActiveView;
            //applicationStore.ActiveViewUpdated += UpdateContent;

            UpdateViewSetup(Store.ViewSetup);
            Store.ViewSetupUpdated += UpdateViewSetup;
        }

        private void UpdateViewSetup(ViewSetups viewSetup)
        {
            switch(viewSetup)
            {
                case ViewSetups.Auth:
                    Content = ServiceProvider.GetService<AuthViewModel>();
                    WindowWidth = 300;
                    WindowHeight = 500;
                    break;
                case ViewSetups.EmptyProjectList:
                case ViewSetups.DetailedProjectList:
                    Content = ServiceProvider.GetService<MainViewModel>();
                    WindowWidth = 750;
                    WindowHeight = 500;
                    break;
            }
        }
    }
}
