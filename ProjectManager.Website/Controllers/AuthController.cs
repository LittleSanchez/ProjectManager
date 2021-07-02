using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Domain;
using ProjectManager.Website.CQRS.Auth;
using ProjectManager.Website.CQRS.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginQuery query)
        {
            return Ok(Mediator.Send(query));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> Register(RegistrationCommand command)
        {
            return Ok(Mediator.Send(command));
        }


    }
}
