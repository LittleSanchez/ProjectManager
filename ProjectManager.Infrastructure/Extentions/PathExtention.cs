using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Extentions
{
    public static class PathExtention
    {
        private const string DIRECTORY_NAME_REGEX = "(^[A-Za-z0-9.\\[\\]{}\\\\\\-_]+)";


        public static string CombineWithJSPath(this string s, string path)
        {
            var resultPath = s;
            if (path.StartsWith("./"))
                path = path.Substring(2);
            path = path.Replace("/", "\\");

            while (path.Length > 0)
            {
                if (path.StartsWith("..\\"))
                {
                    resultPath = Path.GetDirectoryName(resultPath);
                    path = path.Substring(3);
                }
                else if (path.StartsWith("\\"))
                {
                    path = path.Substring(1);
                }
                else if (Regex.IsMatch(path, DIRECTORY_NAME_REGEX))
                {
                    var match = Regex.Match(path, DIRECTORY_NAME_REGEX).Groups[1].Value;
                    resultPath = Path.Combine(resultPath, match);
                    path = path.Substring(match.Length);
                }
            }

            return resultPath;
        }
    }
}
