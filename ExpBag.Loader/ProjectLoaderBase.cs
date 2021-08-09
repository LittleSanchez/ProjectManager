using ExpBag.Domain.Constants;
using ExpBag.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpBag.Loader
{
    public class ProjectLoaderBase
    {
        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }

        public ProjectLoaderBase()
        {
            AvailableExtentions = LoaderConfig.Instance.AvailableExtentions;
            IgnoredNames = LoaderConfig.Instance.IgnoredNames;
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

    }
}
