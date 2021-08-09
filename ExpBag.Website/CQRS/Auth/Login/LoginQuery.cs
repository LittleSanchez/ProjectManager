using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website.CQRS.Auth.Login
{
    public class LoginQuery : IRequest<UserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
