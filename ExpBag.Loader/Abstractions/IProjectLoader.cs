using ExpBag.Domain;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Loader.Abstractions
{
    public interface IProjectLoader
    {
        

        ProjectInfo Load(ProjectInfo project);
    }
}
