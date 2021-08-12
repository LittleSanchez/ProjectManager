using ExpBag.Domain;
using ExpBag.Domain.Models;
using ExpBag.Domain.ModuleInfoTypes.Npm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Abstractions
{

    public interface IProjectModuleCompiler
    {
        Task<ModuleInfo> CompileAsync(ProjectInfo project, object options);
    }
}
