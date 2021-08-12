using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpBag.Loader.Constants
{
    public class NpmLoaderConfig
    {
        public const string CONFIG_TYPE = "npm";
        public static NpmLoaderConfig Instance { get; private set; } = NpmLoaderConfig.Load();

        public List<string> AvailableExtentions { get; set; }
        public List<string> IgnoredNames { get; set; }


        private static NpmLoaderConfig Load()
        {
            if (Instance == null)
            {
                var configFilePath = Constants.Instance.LoaderConfigFiles.First(x => x["type"].ToObject<string>() == CONFIG_TYPE)["name"].ToObject<string>();
                Instance = JsonConvert.DeserializeObject<NpmLoaderConfig>(File.ReadAllText(configFilePath));
            }
            return Instance;
        }

    }
}
