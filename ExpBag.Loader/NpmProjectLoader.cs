using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ExpBag.Loader.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExpBag.Application.Interfaces;
using ExpBag.Application.Constans;

namespace ExpBag.Loader
{
    public class NpmProjectLoader : IProjectLoader
    {
        private readonly IFetchService FetchService;
        private readonly string Url = NetworkConfig.ApiModulesPath;

        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }
        public string ExpbagProjectFolderName { get; set; }
        public string ExpbagConfigFileName { get; set; }

        public NpmProjectLoader(IFetchService _fetchService)
        {
            AvailableExtentions = NpmLoaderConfig.Instance.AvailableExtentions;
            IgnoredNames = NpmLoaderConfig.Instance.IgnoredNames;
            ExpbagConfigFileName = NpmLoaderConfig.Instance.ExpbagConfigFileName;
            ExpbagProjectFolderName = NpmLoaderConfig.Instance.ExpbagProjectFolderName;

            FetchService = _fetchService;
        }


        public List<string> ComponentsSelector(string root)
        {
            List<string> components = new List<string>();

            var subdirectories = Directory.GetDirectories(root).Where(x => IgnoredNames.Count(y => Regex.IsMatch(Path.GetFileName(x), y)) <= 0).Select(x => Path.Combine(root, x)).ToList();
            foreach (var item in subdirectories)
            {
                components.AddRange(ComponentsSelector(item));
            }
            components.AddRange(Directory.GetFiles(root).Where(x => AvailableExtentions.Contains(Path.GetExtension(x))).ToList());
            return components;
        }


        public async Task<ProjectInfo> SaveStoredModules(ProjectInfo project)
        {
            foreach (var module in project.ExpModules.Where(x => x.IsDownloaded == false))
            {
                foreach (var file in module.IncludedFiles.Where(x => x.IsLoaded == false))
                {
                    await FetchService.DownloadFile(Path.Combine(
                        Url, "download", file.Id.ToString()),
                        Path.Combine(
                            project.RootPath,
                            ExpbagProjectFolderName,
                            module.ModuleName,
                            file.FilePath
                            ));
                    file.IsLoaded = true;
                }
                module.IsDownloaded = true;
                module.IsLoaded = true;
            }
            var storedModulesJson = JsonConvert.SerializeObject(project.ExpModules);
            return project;
        }
        public async Task<ProjectInfo> LoadProjectAsync(ProjectInfo project)
        {
            var iterateComponents = ComponentsSelector(project.RootPath).ToList();
            Console.WriteLine(string.Join("\n", iterateComponents));
            project.Components = iterateComponents.Select(x => new ExpModuleFileDTO
            {
                FileName = Path.GetFileName(x),
                FilePath = x
            }).ToList();

            var packageJsonPath = Path.Combine(project.RootPath, "package.json");
            var packageJsonData = JObject.Parse(await File.ReadAllTextAsync(packageJsonPath));
            project.ProjectName = packageJsonData.Value<string>("name");

            var expbagConfigPath = Path.Combine(project.RootPath, ExpbagProjectFolderName, ExpbagConfigFileName);

            if (!File.Exists(expbagConfigPath))
            {
                Directory.CreateDirectory(Path.Combine(project.RootPath, ExpbagProjectFolderName));
                File.Create(expbagConfigPath);
            }



            return project;
        }

        public async Task<ProjectInfo> GetStoredModules(ProjectInfo projectInfo)
        {
            var expFileData = await File.ReadAllTextAsync(Path.Combine(ExpbagProjectFolderName, ExpbagConfigFileName));
            var storedModules = JsonConvert.DeserializeObject<List<ExpModuleStored>>(expFileData);
            projectInfo.ExpModules.Clear();
            foreach (var expModule in storedModules)
            {
                projectInfo.ExpModules.Add(expModule);
            }
            return projectInfo;
        }

        public async Task LoadModuleAsync(ProjectInfo projectInfo)
        {
            var expFileData = await File.ReadAllTextAsync(Path.Combine(ExpbagProjectFolderName, ExpbagConfigFileName));
            var storedModules = JsonConvert.DeserializeObject<List<ExpModuleStored>>(expFileData);

            var packageFileData = await File.ReadAllTextAsync(Path.Combine(projectInfo.RootPath, "package.json"));
            var packageModel = JObject.Parse(packageFileData);

            foreach (var module in storedModules.Where(x => x.IsLoaded == false))
            {

                packageModel["dependencies"].Append(JToken.Parse($"\"{module.ModuleName}\": \"file:{Path.Combine(ExpbagProjectFolderName, module.ModuleName)}\""));
            }

            var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = projectInfo.RootPath,
                FileName = "C:\\Windows\\system32\\cmd.exe",
                RedirectStandardInput = true,
                CreateNoWindow = true
            });
            var processInput = process.StandardInput;
            if (storedModules.Where(x => x.IsLoaded == false).Count() == 0)
            {
                processInput.WriteLine("npm install --save");
            }
            else
            {
                processInput.WriteLine("npm update");
            }
            process.WaitForExit();
        }
    }
}
