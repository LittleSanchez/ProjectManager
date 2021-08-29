using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using ExpBag.EFData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpBag.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController
    {
        
        public async Task<IActionResult> AddModule([FromBody] ExpModuleDTO moduleDto)
        {
            var module = Mapper.Map<ExpModule>(moduleDto);
            Debug.WriteLine($"Module {module.ModuleName} is loaded successfuly");
            return Ok();
        }
    }
}
