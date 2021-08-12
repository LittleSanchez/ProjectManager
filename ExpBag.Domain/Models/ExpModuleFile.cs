using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.Models
{
    public class ExpModuleFile
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsLoaded { get; set; }
    }
}
