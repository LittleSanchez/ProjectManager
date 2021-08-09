using ExpBag.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Environment
{
    public class TempContoller : ITempController
    {
        public const string TempFolderName = ".trash_bag";

        static TempContoller()
        {
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), TempFolderName));
        }

        public string CreateTempDirectory(string moduleName)
        {
            var path = Path.Combine(Path.GetTempPath(), TempFolderName, moduleName + Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
