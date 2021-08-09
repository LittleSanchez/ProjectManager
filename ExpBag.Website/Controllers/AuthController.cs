using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpBag.Domain;
using ExpBag.Website.CQRS.Auth;
using ExpBag.Website.CQRS.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> Register(RegistrationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


    }
}
