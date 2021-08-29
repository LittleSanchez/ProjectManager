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
using ExpBag.Domain.DTO;
using ExpBag.Domain.CQRSObjects;

namespace ExpBag.Website.Controllers
{
    //Added comment on master
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("registration")] // Change in ClientApp !!!!!!! (register - registration)
        public async Task<ActionResult<UserDTO>> Register(RegistrationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


    }
}
