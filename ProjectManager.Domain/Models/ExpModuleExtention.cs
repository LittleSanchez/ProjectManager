using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class ExpModuleExtention
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public bool UsedInExpModule { get; set; }     

    }
}
