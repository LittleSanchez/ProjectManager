using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.Models
{
    public class ExpModule
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ModuleInfo { get; set; } // JSON Object
        public string ModuleInfoType { get; set; } // Type of JSON Object
        public string ServerLocation { get; set; }

        public AppUser User { get; set; }
        public ExpModuleExtention ModuleExtention { get; set; }
    }
}
