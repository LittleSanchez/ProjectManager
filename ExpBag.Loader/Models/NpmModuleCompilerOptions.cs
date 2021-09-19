using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Models
{
    public class NpmModuleCompilerOptions
    {
        public string TargetFile { get; set; } = null;
        public string DestinationFolder { get; set; } = null;
        public string ModuleVersion { get; set; } = null;
        public string ModuleName { get; set; } = null;

    }
}
