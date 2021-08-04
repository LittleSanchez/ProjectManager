using ProjectManager.Domain;
using ProjectManager.Domain.Models;
using ProjectManager.Loader.Abstractions;
using System;
using System.IO;
using System.Linq;

namespace ProjectManager.Loader
{
    public class ProjectSelector : IProjectSelector
    { 
        public ProjectInfo OpenProject(string path)
        {
            if (!Directory.Exists(path) && File.Exists(path))
            {
                path = new FileInfo(path).DirectoryName;
            }
            if (Directory.Exists(Path.GetPathRoot(path)))
            {
                var projectInfo = new ProjectInfo
                {
                    ProjectName = Path.GetDirectoryName(path),
                    RootPath = path
                };
                Console.WriteLine($"Project name: {projectInfo.ProjectName}, root path: {projectInfo.RootPath}");
                return projectInfo;
            }
            throw new Exception("Invalid project folder!"); //TODO: Make special exception class
        }
    }
}
