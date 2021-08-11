using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GitAutoupdateController : BaseController
    {
        private const string UBUNTU_OS_VARIABLE = "UBUNT";
        private const string WINDOWS_OS_VARIABLE = "Windows_NT";



        [HttpPost("update")]
        public async Task<ActionResult> Update()
        {
            switch(Environment.GetEnvironmentVariable("OS"))
            {

            }
            return Ok();
        }
    }
}
