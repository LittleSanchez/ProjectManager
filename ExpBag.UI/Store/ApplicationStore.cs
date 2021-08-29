using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
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
        //private ViewModelBase? _activeView;
        private ViewSetups _viewSetup;

        public event Action<UserDTO> ProfileUpdated;
        //public event Action<ViewModelBase> ActiveViewUpdated;
        public event Action<ViewSetups> ViewSetupUpdated;


        public UserDTO? Profile
        {
            get => _profile; set
            {
                _profile = value;
                //Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                //{
                    ProfileUpdated?.Invoke(value);
                //});
            }
        }
        //public ViewModelBase? ActiveView
        //{
        //    get => _activeView; set
        //    {
        //        _activeView = value;
        //        //Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
        //        //{
        //            ActiveViewUpdated?.Invoke(value);
        //        //});
        //    }
        //}
        public ViewSetups ViewSetup
        {
            get => _viewSetup;
            set {
                _viewSetup = value;
                //Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                //{
                    ViewSetupUpdated?.Invoke(value);
                //});
            }
        }


        public ApplicationStore()
        {
            
        }

    }
}
