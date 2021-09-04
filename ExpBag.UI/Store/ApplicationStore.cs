﻿using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.Store
{

    public class ApplicationStore
    {
        private UserDTO? _profile;
        private ViewSetups _viewSetup;
        private List<ProjectInfo> _projects;

        public event Action<UserDTO> ProfileUpdated;
        public event Action<ViewSetups> ViewSetupUpdated;
        public event Action<List<ProjectInfo>> ProjectsUpdated;

        public UserDTO? Profile
        {
            get => _profile; set
            {
                _profile = value;
                ProfileUpdated?.Invoke(value);
            }
        }
        public ViewSetups ViewSetup
        {
            get => _viewSetup;
            set
            {
                _viewSetup = value;
                ViewSetupUpdated?.Invoke(value);
            }
        }

        public List<ProjectInfo> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                ProjectsUpdated?.Invoke(value);
            }
        }


        public ApplicationStore()
        {
            Projects = new List<ProjectInfo>();
        }

    }
}
