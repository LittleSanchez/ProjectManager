using ExpBag.Domain;
using ExpBag.Domain.Constants;
using ExpBag.Loader.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpBag.Loader.Abstractions
{
    public interface IProjectLoader
    {
        List<string> AvailableExtentions { get; set; }
        List<string> IgnoredNames { get; set; }


        List<string> ComponentsSelector(string root);
        ProjectInfo Load(ProjectInfo project);


    }
}
