using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.Domain.Models;
using ExpBag.Domain.ModuleInfoTypes.Npm;
using ExpBag.Infrastructure.Extentions;
using ExpBag.Loader.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpBag.Loader
{
    public class NpmModuleCompilerOptions
    {
        public string TargetFile { get; set; } = null;
        public string DestinationFolder { get; set; } = null;
        public string ModuleVersion { get; set; } = null;
        public string ModuleName { get; set; } = null;

    }


    public class NpmModuleCompiler : IProjectModuleCompiler
    {
        //private const string FILE_REGEX = "[\'\"]{1}(\\.+\\/.+)[\'\"]{1}";
        private const string MODULE_REGEX = "[\'\"]{1}(.+)[\'\"]{1}";

        private const string SUPER_FILE_REGEX = "(require)*(import)*[\\sa-zA-Z0-9{},.]*(from)*[\\sa-zA-Z0-9{},.]*.*[\'\"]{1}(\\.+\\/.+)[\'\"]{1}";

        private readonly ITempController tempController;

        public NpmModuleCompiler(ITempController _tempController)
        {
            tempController = _tempController;
        }

        public async Task<ModuleInfo> CompileAsync(ProjectInfo project, object options)
        {
            if (!(options is NpmModuleCompilerOptions))
            {
                throw new InvalidCastException("Can't cast \'object\' to \'NpmModuleCompilerOptions\'");
            }
            var compilerOptions = options as NpmModuleCompilerOptions;
            var moduleName = Path.GetFileNameWithoutExtension(compilerOptions.TargetFile);

            //string moduleDirectory = tempController.CreateTempDirectory(moduleName);
            string moduleDirectory = compilerOptions.DestinationFolder;
            var files = await GetIncludedFilesAsync(compilerOptions.TargetFile);
            var modules = await GetIncludedModulesAsync(compilerOptions.TargetFile);
            files.Add(compilerOptions.TargetFile);

            //Moving files from project directory to temp
            files = await MoveFilesAsync(files, project.RootPath, moduleDirectory);

            //Creating package module file
            string packagePath = await CreatePackageFileAsync(moduleDirectory, JObject.Parse("{" +
                $"\"name\": \"{compilerOptions.ModuleName}\"," + 
                $"\"version\": \"{compilerOptions.ModuleVersion}\"," + 
                $"\"version\": \"{compilerOptions.ModuleVersion}\"," + 
                $"\"type\": \"module\"" + //  <<<< IMPORTANT!!!! >>>>> 
                "}"));
            files.Add(packagePath);


            return new NpmModuleInfo
            {
                IncludedFiles = files,
                IncludedNpmModules = modules,
                ModuleName = compilerOptions.ModuleName,
                Version = compilerOptions.ModuleVersion,
            };
        }

        //Get Included Files =======================
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


        //Get Included Modules =====================
        protected List<string> GetIncludedModules(string filePath)
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
                        var match = Regex.Match(line, SUPER_FILE_REGEX);
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
                        var match = Regex.Match(line, SUPER_FILE_REGEX);
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

        //Move Files ===============================
        protected List<string> MoveFiles(List<string> files, string rootPath, string destinationDirectory)
        {
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var insidePath = file.Substring(rootPath.Length + 1);
                var tempPath = Path.Combine(destinationDirectory, insidePath);
                if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                }
                File.Copy(file, tempPath);
                files[i] = tempPath.Substring(destinationDirectory.Length + 1);
            }
            return files;
        }
        protected async Task<List<string>> MoveFilesAsync(List<string> files, string rootPath, string destinationDirectory)
        {
            return await Task.Run<List<string>>(() =>
            {
                return MoveFiles(files, rootPath, destinationDirectory);
            });
        }

        //Create Package File ======================
        protected string CreatePackageFile(string moduleDirectory, JObject options)
        {
            var packageFilePath = Path.Combine(moduleDirectory, "package.json");

            var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = moduleDirectory,
                FileName = "C:\\Windows\\system32\\cmd.exe",
                RedirectStandardInput = true,
                CreateNoWindow = true
            });
            var processInput = process.StandardInput;
            processInput.WriteLine("npm init --yes & exit");
            process.WaitForExit();
            var packageData = JObject.Parse(File.ReadAllText(packageFilePath));
            foreach(var item in options)
            {
                packageData[item.Key] = item.Value;
            }
            File.WriteAllText(packageFilePath, packageData.ToString());
            return packageFilePath;
        }

        protected async Task<string> CreatePackageFileAsync(string moduleDirectory, JObject options)
        {
            return await Task.Run<string>(() =>
            {
                return CreatePackageFile(moduleDirectory, options);
            });
        }

    }

}
