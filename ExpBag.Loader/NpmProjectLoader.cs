using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using ExpBag.Loader.Abstractions;
using ExpBag.Loader.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpBag.Loader
{
    public class NpmProjectLoader : IProjectLoader
    {
        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }

        public NpmProjectLoader() 
        {
            AvailableExtentions = NpmLoaderConfig.Instance.AvailableExtentions;
            IgnoredNames = NpmLoaderConfig.Instance.IgnoredNames;
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

        public ProjectInfo Load(ProjectInfo project)
        {
            var iterateComponents = ComponentsSelector(project.RootPath).ToList();
            Console.WriteLine(string.Join("\n", iterateComponents));
            project.Components = iterateComponents.Select(x => new ExpModuleFileDTO
            {
                FileName = Path.GetFileName(x),
                FilePath = x
            }).ToList();
            return project;
        }

       
    }
}
