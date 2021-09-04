using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain
{
    public class ProjectInfo
    {
        public List<ExpModuleFileDTO> Components { get; set; }
        public ObservableCollection<ModuleInfo> ExpModules { get; set; }
        public string ProjectName { get; set; }
        public string RootPath { get; set; }

        public ProjectInfo()
        {
            Components = new List<ExpModuleFileDTO>();
            ExpModules = new ObservableCollection<ModuleInfo>();
        }
    }
}
