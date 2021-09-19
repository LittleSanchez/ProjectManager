using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using ExpBag.EFData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpBag.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController
    {
        private readonly DataContext context;
        private readonly UserManager<AppUser> userManager;


        public ModuleController(DataContext _context,
            UserManager<AppUser> _userManager
             )
        {
            context = _context;
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpModuleDTO>>> GetModules()
        {
            return Ok(context.Modules
                .Include(x => x.IncludedFiles)
                .Select(x => Mapper.Map<ExpModuleDTO>(x)));
        }

        [HttpPost]
        public async Task<ActionResult<ExpModuleDTO>> AddModule([FromBody] ExpModuleDTO moduleDto)
        {
            var module = Mapper.Map<ExpModule>(moduleDto);

            var user = await userManager.FindByIdAsync(User.Claims.Where(x => x.Type.Equals("id")).Select(x => x.Value).FirstOrDefault());
            module.User = user;
            context.Add(module);

            foreach (var file in module.IncludedFiles)
            {
                file.Module = module;
                file.IsLoaded = false;
                context.ModuleFiles.Add(file);
            }
            await context.SaveChangesAsync();
            moduleDto = Mapper.Map<ExpModuleDTO>(module);
            return Ok(moduleDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateModule(int id, [FromBody] ExpModuleDTO moduleDto)
        {
            var mappedModule = Mapper.Map<ExpModule>(moduleDto);
            var module = await context.Modules.FindAsync(id);
            module.ModuleExtention = mappedModule.ModuleExtention;
            module.ModuleInfo = mappedModule.ModuleInfo;
            module.ModuleInfoType = mappedModule.ModuleInfoType;
            module.ModuleName = mappedModule.ModuleName;

            context.Update(module);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteModule(int id)
        {
            var module = await context.Modules.FindAsync(id);
            context.Remove(module);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("upload/{moduleId}")]
        public async Task<ActionResult<ExpModuleDTO>> UploadFiles(int moduleId, [FromForm] IFormCollection formData)
        {
            var module = context.Modules.Include(x => x.User).Include(x => x.IncludedFiles).Where(x => x.Id == moduleId).First();
            var workingDirectory = $"{module.ModuleName}_{module.User.UserName}"; /////// ------------------- WORKING DIRECTORY
            var files = HttpContext.Request.Form.Files;
            foreach (var file in files)
            {
                var expModuleFile = module.IncludedFiles.Where(x => x.Id == int.Parse(file.Name)).First();
                if (expModuleFile == null)
                {
                    continue;
                }
                var uploads = Path.Combine(Configuration.GetValue<string>("UploadFolders:ExpModuleFiles"), workingDirectory);
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploads, expModuleFile.FilePath);
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    }
                    string fileName = file.FileName;

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    expModuleFile.IsLoaded = true;
                }
            }
            await context.SaveChangesAsync();
            var moduleDTO = Mapper.Map<ExpModuleDTO>(module);
            return moduleDTO;
        }


        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            var file = context.ModuleFiles.Find(fileId);
            Stream stream = System.IO.File.OpenRead(file.FilePath);

            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }
    }
}
