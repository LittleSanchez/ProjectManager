using ProjectManager.Domain.Constants;
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
    public class ProjectLoaderBase
    {
        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }

        public ProjectLoaderBase(LoaderConfig config)
        {
            AvailableExtentions = config.AvailableExtentions;
            IgnoredNames = config.IgnoredNames;
        }

        public List<string> ComponentsSelector(string root)
        {
            List<string> components = new List<string>();

            var subdirectories = Directory.GetDirectories(root).Where(x => IgnoredNames.Count(y => Regex.IsMatch(Path.GetFileName(x), y)) <= 0).ToList();
            foreach (var item in subdirectories)
            {
                components.AddRange(ComponentsSelector(item));
            }
            components.AddRange(Directory.GetFiles(root).Where(x => AvailableExtentions.Contains(Path.GetExtension(x))).ToList());
            return components;
        }

    }
}
