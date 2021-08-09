using ExpBag.Domain;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Loader.Abstractions
{
    public interface IProjectSelector
    {
        ProjectInfo OpenProject(string path);
    }
}
