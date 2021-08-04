using ProjectManager.Domain;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Loader.Abstractions
{
    public interface IProjectLoader
    {
        

        ProjectInfo Load(ProjectInfo project);
    }
}
