using ExpBag.Domain;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Abstractions
{

    public class ProjectModuleCompilerOptions
    {
        public String ModuleName { get; set; }
        public String MainFile { get; set; }
        public double Version { get; set; }

    }

    public interface IProjectModuleCompiler
    {
        Task<ModuleInfo> CompileAsync(ProjectInfo project, string targetFile, string destinationFolder);
    }
}
