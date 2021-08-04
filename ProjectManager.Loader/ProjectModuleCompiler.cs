using ProjectManager.Application.Interfaces;
using ProjectManager.Domain;
using ProjectManager.Domain.Models;
using ProjectManager.Infrastructure.Extentions;
using ProjectManager.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectManager.Loader
{
    public class ProjectModuleCompiler : IProjectModuleCompiler
    {
        private const string FILE_REGEX = "[\'\"]{1}(\\.+\\/.+)[\'\"]{1}";
        private const string MODULE_REGEX = "[\'\"]{1}(.+)[\'\"]{1}";

        private const string SUPER_FILE_REGEX = "(require)*(import)*[\\sa-zA-Z0-9{},.]*(from)*[\\sa-zA-Z0-9{},.]*.*[\'\"]{1}(\\.+\\/.+)[\'\"]{1}";

        private readonly ITempController tempController;

        public ProjectModuleCompiler(ITempController _tempController)
        {
            tempController = _tempController;
        }

        public async Task<ModuleInfo> CompileAsync(ProjectInfo project, string targetFile)
        {
            var moduleName = Path.GetFileNameWithoutExtension(targetFile);

            string moduleDirectory = tempController.CreateTempDirectory(moduleName);
            var files = await GetIncludedFilesAsync(targetFile);
            var modules = await GetIncludedModulesAsync(targetFile);
            files.Add(targetFile);
            for(int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var insidePath = file.Substring(project.RootPath.Length + 1);
                var tempPath = Path.Combine(moduleDirectory, insidePath);
                if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                }
                File.Copy(file, tempPath);
                files[i] = tempPath;
            }

            return new ModuleInfo
            {
                IncludedFiles = files,
                IncludedModules = modules,
                ModuleName = moduleName,
                RootPath = moduleDirectory
            };
        }



        //protected List<string> GetIncludedFiles(string filePath)
        //{
        //    filePath = Path.GetFullPath(filePath);

        //    var currentDirectory = Path.GetDirectoryName(filePath);
        //    var files = new List<string>();
        //    using (var streamReader = File.OpenText(filePath))
        //    {
        //        while (!streamReader.EndOfStream)
        //        {
        //            string line = streamReader.ReadLine();
        //            if (line.Contains("import"))
        //            {
        //                var match = Regex.Match(line, FILE_REGEX);
        //                if (match.Success)
        //                {
        //                    var includedFileName = match.Groups[1].Value;
        //                    if (Path.GetExtension(includedFileName).Length <= 0)
        //                    {
        //                        var tmpFileName = Directory.GetFiles(Path.GetDirectoryName(currentDirectory.CombineWithJSPath(includedFileName))).Where(x => x.Contains(Path.GetFileName(includedFileName))).First();
        //                        includedFileName = tmpFileName;
        //                    }
        //                    files.Add(includedFileName);
        //                    files.AddRange(GetIncludedFiles(includedFileName));
        //                }
        //            }
        //            else if (line.Contains("require"))
        //            {
        //                var match = Regex.Match(line, FILE_REGEX);
        //                if (match.Success)
        //                {
        //                    var includedFileName = match.Groups[1].Value;
        //                    if (Path.GetExtension(includedFileName).Length <= 0)
        //                    {
        //                        var tmpFileName = Directory.GetFiles(Path.GetDirectoryName(currentDirectory.CombineWithJSPath(includedFileName))).Where(x => x.Contains(Path.GetFileName(includedFileName))).First();
        //                        includedFileName = tmpFileName;
        //                    }
        //                    files.Add(includedFileName);
        //                    files.AddRange(GetIncludedFiles(includedFileName));
        //                }
        //            }
        //        }
        //    }
        //    return files;
        //}

        protected List<string> GetIncludedFiles(string filePath)
        {
            filePath = Path.GetFullPath(filePath);

            var currentDirectory = Path.GetDirectoryName(filePath);
            var files = new List<string>();
            var fileData = File.ReadAllText(filePath);

            var matches = Regex.Matches(fileData, SUPER_FILE_REGEX, RegexOptions.Multiline, TimeSpan.FromSeconds(10));
            foreach (Match match in matches)
            {
                var includedFileName = match.Groups[match.Groups.Count - 1].Value;
                if (Path.GetExtension(includedFileName).Length <= 0)
                {
                    var tmpFileName = Directory.GetFiles(Path.GetDirectoryName(currentDirectory.CombineWithJSPath(includedFileName))).Where(x => x.Contains(Path.GetFileName(includedFileName))).First();
                    includedFileName = tmpFileName;
                }
                files.Add(includedFileName);
                files.AddRange(GetIncludedFiles(includedFileName));
            }
            return files;
        }

        protected async Task<List<string>> GetIncludedFilesAsync(string filePath)
        {
            return await Task.Run<List<string>>(() =>
            {
                return GetIncludedFiles(filePath);
            });
        }



        protected  List<string> GetIncludedModules(string filePath)
        {
            filePath = Path.GetFullPath(filePath);

            var currentDirectory = Path.GetDirectoryName(filePath);
            var modules = new List<string>();
            using (var streamReader = File.OpenText(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line.Contains("import"))
                    {
                        var match = Regex.Match(line, FILE_REGEX);
                        if (match.Success)
                        {
                            var includedFileName = match.Groups[match.Groups.Count - 1].Value;
                            if (Path.GetExtension(includedFileName).Length <= 0)
                            {
                                var tmpFileName = Directory.GetFiles(Path.GetDirectoryName(currentDirectory.CombineWithJSPath(includedFileName))).Where(x => x.Contains(Path.GetFileName(includedFileName))).First();
                                includedFileName = tmpFileName;
                            }
                            modules.AddRange(GetIncludedModules(includedFileName));
                            continue;
                        }
                        match = Regex.Match(line, MODULE_REGEX);
                        if (match.Success)
                        {
                            modules.Add(match.Groups[match.Groups.Count - 1].Value);
                        }
                    }
                    else if (line.Contains("require"))
                    {
                        var match = Regex.Match(line, FILE_REGEX);
                        if (match.Success)
                        {
                            var includedFileName = match.Groups[match.Groups.Count - 1].Value;
                            if (Path.GetExtension(includedFileName).Length <= 0)
                            {
                                var tmpFileName = Directory.GetFiles(Path.GetDirectoryName(currentDirectory.CombineWithJSPath(includedFileName))).Where(x => x.Contains(Path.GetFileName(includedFileName))).First();
                                includedFileName = tmpFileName;
                            }
                            modules.AddRange(GetIncludedModules(includedFileName));
                            continue;
                        }
                        match = Regex.Match(line, MODULE_REGEX);
                        if (match.Success)
                        {
                            modules.Add(match.Groups[match.Groups.Count - 1].Value);
                        }
                    }
                }
            }
            return modules
                //Gets only names of modules and makes them distinct
                .Select(x => Regex.Match(x, "([\\w\\-_]+)\\\\*.*").Groups[1].Value).Distinct().ToList();
        }

        protected async Task<List<string>> GetIncludedModulesAsync(string filePath)
        {
            return await Task.Run<List<string>>(() =>
            {
                return GetIncludedModules(filePath);
            });
        }
    }
}
