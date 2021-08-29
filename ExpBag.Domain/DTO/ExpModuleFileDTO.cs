using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.DTO
{
    public class ExpModuleFileDTO
    {
        public int? Id { get; set; } = null;
        public int? ModuleId { get; set; } = null;
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool IsLoaded { get; set; }

        public string ModuleName { get; set; }
    }
}
