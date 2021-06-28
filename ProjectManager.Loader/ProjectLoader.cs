using ProjectManager.Domain;
using ProjectManager.Domain.Constants;
using ProjectManager.Domain.Models;
using ProjectManager.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Loader
{
    public class ProjectLoader : ProjectLoaderBase, IProjectLoader
    {

        public ProjectLoader(LoaderConfig config) : base(config)
        {
            
        }

        public ProjectInfo Load(ProjectModel project)
        {
            var projectInfo = new ProjectInfo();
            var iterateComponents = ComponentsSelector(project.RootPath).Select(x => x.Substring(project.RootPath.Length + 1)).ToList();
            Console.WriteLine(string.Join("\n", iterateComponents));
            projectInfo.Components = iterateComponents.Select(x => new ProjectComponent
            {
                ComponentName = Path.GetFileName(x),
                FilePath = x
            }).ToList();
            return projectInfo;
        }

       
    }
}
