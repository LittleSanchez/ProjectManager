using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Loader.Abstractions
{
    public interface IProjectSelector
    {
        ProjectModel OpenProject(string path);
    }
}
