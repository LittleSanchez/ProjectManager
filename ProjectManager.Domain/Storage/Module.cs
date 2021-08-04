using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Storage
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }






        public List<ModuleFile> ModuleFiles { get; set; }
        public AppUser User { get; set; }
    }
}
