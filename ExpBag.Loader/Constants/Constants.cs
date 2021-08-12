using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Constants
{
    // config files are located in ExpBag.UI 

    public class Constants
    {
        public const string CONSTANTS_FILE_PATH = "constants.json";
        public static Constants Instance { get; private set; } = Load();

        public JArray LoaderConfigFiles { get; set; }

        private static Constants Load()
        {
            if (Instance == null)
            {
                Instance = JsonConvert.DeserializeObject<Constants>(File.ReadAllText(CONSTANTS_FILE_PATH));
            }
            return Instance;
        }
    }
}
