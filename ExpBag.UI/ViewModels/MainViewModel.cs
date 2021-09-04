﻿using ExpBag.UI.Startup;
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

namespace ExpBag.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;

        private readonly ApplicationStore Store;


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

        public MainViewModel()
        {
            Store = ServiceProvider.GetService<ApplicationStore>();

            Store.ViewSetupUpdated += UpdateViewSetup;
            UpdateViewSetup(Store.ViewSetup);
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
                default:
                    Left = null;
                    Right = null;
                    break;
            }
        }
    }
}
