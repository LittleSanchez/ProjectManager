using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.ModuleInfoTypes.Npm
{
    public class NpmModuleInfo : ModuleInfo
    {
        public string Version { get; set; }

        public List<string> IncludedNpmModules { get; set; }

    }
}
