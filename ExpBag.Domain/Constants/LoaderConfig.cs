using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpBag.Domain.Constants
{
    public class LoaderConfig
    {

        public static LoaderConfig Instance { get; set; } = LoaderConfig.Load();

        public List<string> ProjectTypes { get; set; }
        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }


        private static LoaderConfig Load()
        {
            var inst = Instance ?? (Instance = (LoaderConfig)JsonSerializer.Deserialize(File.ReadAllText(Constants.LoaderConfigFileName), typeof(LoaderConfig)));
            return inst;
        }

    }
}
