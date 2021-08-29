using ExpBag.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Domain.CQRSObjects
{
    public class LoginQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
