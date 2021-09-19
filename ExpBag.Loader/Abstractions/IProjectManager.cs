using ExpBag.Domain;
using ExpBag.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Abstractions
{
    public interface IProjectManager
    {
        void AddModule(ProjectInfo projectInfo, ExpModuleDTO moduleDTO);
        void DeleteModule(ProjectInfo projectInfo, int moduleId);

        void ReloadModules(ProjectInfo projectInfo);
    }
}
