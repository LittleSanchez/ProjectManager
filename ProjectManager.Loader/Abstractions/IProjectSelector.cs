using ProjectManager.Domain;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Loader.Abstractions
{
    public interface IProjectSelector
    {
        ProjectInfo OpenProject(string path);
    }
}
