using Avalonia.Threading;
using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
using ExpBag.Domain.CQRSObjects;
using ExpBag.Domain.DTO;
using ExpBag.UI.Abstractions;
using ExpBag.UI.Store;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ExpBag.UI.Services
{
    public class AppAuthService : IAppAuthService
    {

        private readonly IAuthService AuthService;
        private readonly ApplicationStore Store;

        private readonly string ServerUrl = NetworkConfig.ServerUrl;
        private readonly string ApiAuthPath = NetworkConfig.ApiAuthPath;
        private string LoginUrl;
        private string RegistrationUrl;

        public AppAuthService(IAuthService authService, ApplicationStore store)
        {
            AuthService = authService;
            Store = store;

            LoginUrl = Path.Combine(ServerUrl, ApiAuthPath, "login");
            RegistrationUrl = Path.Combine(ServerUrl, ApiAuthPath, "registration");
        }

        public void Login(LoginQuery loginQuery)
        {
            Store.Profile = AuthService.Login(loginQuery).Result;
        }

        public async Task LoginAsync(LoginQuery loginQuery)
        {
            Store.Profile = await AuthService.Login(loginQuery);
        }

        public void Register(RegistrationCommand registrationCommand)
        {
            Store.Profile = AuthService.Register(registrationCommand).Result;
        }

        public async Task RegisterAsync(RegistrationCommand registrationCommand)
        {
            Store.Profile = await AuthService.Register(registrationCommand);
        }
    }
}
