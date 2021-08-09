using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.ModuleInfoTypes.Npm
{
    public class NpmModuleInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public List<string> IncludedNpmModules { get; set; }
        public List<string> IncludedFiles { get; set; }

        public NpmModuleInfo()
        {
            if (IncludedFiles == null)
                IncludedFiles = new List<string>();
            if (IncludedNpmModules == null)
                IncludedNpmModules = new List<string>();
        }
    }
}
