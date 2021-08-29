using ExpBag.Domain.CQRSObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.Abstractions
{
    public interface IAppAuthService
    {
        void Login(LoginQuery loginQuery);
        void Register(RegistrationCommand registrationCommand);
        Task LoginAsync(LoginQuery loginQuery);
        Task RegisterAsync(RegistrationCommand registrationCommand);

    }
}
