using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.DTO
{
    public class ExpModuleDTO
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ModuleInfo { get; set; }
        public string ModuleInfoType { get; set; }
        public List<ExpModuleFileDTO> IncludedFiles { get; set; }
        public string UserName { get; set; }


    }
}
