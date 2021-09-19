using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.UI.Services;
using ExpBag.UI.Startup;
using ExpBag.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace ExpBag.UI.Store
{

    public class ApplicationStore
    {
        private readonly IServiceProvider ServiceProvider = ServiceProviderFactory.ServiceProvider;
        



        private UserDTO? _profile;
        private ViewSetups _viewSetup;
        private List<ProjectInfo> _projects;
        private IEnumerable<ExpModuleDTO> _userModules;

        public event Action<UserDTO> ProfileUpdated;
        public event Action<ViewSetups> ViewSetupUpdated;
        public event Action<List<ProjectInfo>> ProjectsUpdated;
        public event Action<IEnumerable<ExpModuleDTO>> UserModulesUpdated;

        public UserDTO? Profile
        {
            get => _profile; set
            {
                _profile = value;
                ProfileUpdated?.Invoke(value);
                StoreLoader.SaveStore(this);
            }
        }
        public ViewSetups ViewSetup
        {
            get => _viewSetup;
            set
            {
                _viewSetup = value;
                ViewSetupUpdated?.Invoke(value);
                StoreLoader.SaveStore(this);
            }
        }

        public List<ProjectInfo> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                ProjectsUpdated?.Invoke(value);
                StoreLoader.SaveStore(this);
            }
        }
        public IEnumerable<ExpModuleDTO> UserModules
        {
            get => _userModules;
            set
            {
                _userModules = value;
                UserModulesUpdated?.Invoke(value);
                StoreLoader.SaveStore(this);
            }
        }

        public ApplicationStore()
        {
            Projects = new List<ProjectInfo>();
        }

    }
}
