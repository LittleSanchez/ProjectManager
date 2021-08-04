using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Storage
{
    public class ModuleFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }


        public Module Module { get; set; }



    }
}
