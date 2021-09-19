using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader
{
    public class ProjectManager : IProjectManager
    {
        public void AddModule(ProjectInfo projectInfo, ExpModuleDTO moduleDTO)
        {
            projectInfo.ExpModules.Add(moduleDTO as ExpModuleStored);
        }

        public void DeleteModule(ProjectInfo projectInfo, int moduleId)
        {

        }

        public void ReloadModules(ProjectInfo projectInfo)
        {
            throw new NotImplementedException();
        }
    }
}
