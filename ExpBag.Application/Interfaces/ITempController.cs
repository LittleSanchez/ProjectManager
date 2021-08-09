using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.Interfaces
{
    public interface ITempController
    {
        string CreateTempDirectory(string moduleName);
    }
}
