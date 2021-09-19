using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Environment
{
    public class AppdataController
    {
        public static string AppDataFolder { get; private set; } = Path.Combine(
                System.Environment.GetEnvironmentVariable("AppData"),
                ".expbag"
            );
        
    }
}
