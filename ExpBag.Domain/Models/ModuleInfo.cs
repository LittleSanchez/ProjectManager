using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Domain.Models
{
    public class ModuleInfo
    {
        public string ModuleName { get; set; }
        public List<string> IncludedFiles { get; set; } = new List<string>();

    }
}
