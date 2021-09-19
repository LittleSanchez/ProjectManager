using ExpBag.Domain;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ExpBag.Loader.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExpBag.Loader
{
    public class ProjectSelector : IProjectSelector
    {

        public string ExpbagProjectFolderName { get; private set; } = NpmLoaderConfig.Instance.ExpbagProjectFolderName;
        public string ExpbagConfigFileName { get; private set; } = NpmLoaderConfig.Instance.ExpbagConfigFileName;
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
                    RootPath = path,
                };
                if (Directory.Exists(Path.Combine(path, ExpbagProjectFolderName)))
                {
                    var configPath = Path.Combine(path, ExpbagProjectFolderName, ExpbagConfigFileName);
                    if (File.Exists(configPath))
                    {
                        var modules = JsonConvert.DeserializeObject<IEnumerable<ExpModuleStored>>(File.ReadAllText(configPath));
                        if (modules != null)
                        {
                            projectInfo.ExpModules.Clear();
                            foreach (var module in modules)
                            {
                                projectInfo.ExpModules.Add(module);
                            }
                        }
                    }
                }
                Console.WriteLine($"Project name: {projectInfo.ProjectName}, root path: {projectInfo.RootPath}");
                return projectInfo;
            }
            throw new Exception("Invalid project folder!"); //TODO: Make special exception class
        }
    }
}
