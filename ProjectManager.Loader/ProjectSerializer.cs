using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;
using ProjectManager.Infrastructure.Extentions;
using ProjectManager.Loader.Abstractions;

namespace ProjectManager.Loader
{
    public class ProjectSerializer : IProjectSerializer
    {
       
        public string Deserialize(string packageBinaryPath, string destFolder)
        {
            throw new NotImplementedException();
        }

        public string Serialize(string filePath, ModuleInfo module)
        {
            //string moduleDirectory = TempController.CreateTempDirectory(module.ModuleName); 
            //var files = GetIncludedFiles(filePath);
            //files.Add(filePath);
            //foreach(var file in files)
            //{
            //    var fileInfo = new FileInfo(file);
            //    //Directory.CreateDirectory()
            //    //fileInfo.
            //}
            return null;
        }

        
        
    }
}
