using ExpBag.Domain;
using ExpBag.Domain.Constants;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader
{
    public class ProjectLoader : ProjectLoaderBase, IProjectLoader
    {

        public ProjectLoader() 
        {
            
        }

        public ProjectInfo Load(ProjectInfo project)
        {
            var iterateComponents = ComponentsSelector(project.RootPath).ToList();
            Console.WriteLine(string.Join("\n", iterateComponents));
            project.Components = iterateComponents.Select(x => new ProjectComponent
            {
                ComponentName = Path.GetFileName(x),
                FilePath = x
            }).ToList();
            return project;
        }

       
    }
}
