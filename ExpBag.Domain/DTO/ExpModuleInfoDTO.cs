using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.DTO
{
    public class ExpModuleInfoDTO
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public bool UsedInExpModule { get; set; }
        public bool FilesLoaded { get; set; }
    }
}
