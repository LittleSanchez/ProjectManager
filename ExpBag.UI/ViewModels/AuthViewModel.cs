using ExpBag.UI.Startup;
using ExpBag.UI.Store;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive;
using ExpBag.Application.Interfaces;
using ExpBag.Domain.CQRSObjects;
using ExpBag.UI.Abstractions;
using System.Threading;
using System.Diagnostics;
using Avalonia.Threading;

namespace ExpBag.UI.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private readonly IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;
        private readonly IAppAuthService authService;
        private readonly IAppViewService viewService;

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        //CommandProperties

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public AuthViewModel()
        {
            authService = serviceProvider.GetService<IAppAuthService>();
            viewService = serviceProvider.GetService<IAppViewService>();

            LoginCommand = ReactiveCommand.CreateFromTask(Login);
        }

        //Commands

        public async Task Login()
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine(Thread.CurrentThread.Name);
            await authService.LoginAsync(new LoginQuery
            {
                Email = Email,
                Password = Password
            });
            viewService.Show(Application.Constans.ViewSetups.EmptyProjectList);
        }

    }
}
