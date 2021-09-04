using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.ModuleInfoTypes.Npm
{
    public class NpmModuleFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsLoaded { get; set; }
    }

    public class NpmModuleInfo : ModuleInfo
    {         
        public string Version { get; set; }

        public List<string> IncludedNpmModules { get; set; }
        public List<string> IncludedFiles { get; set; }

    }
}
