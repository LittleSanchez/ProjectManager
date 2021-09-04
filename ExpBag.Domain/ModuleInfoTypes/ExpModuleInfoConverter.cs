using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain.ModuleInfoTypes
{
    public static class ExpModuleInfoConverter
    {
        public static T Convert<T>(ModuleInfo moduleInfo) where T : ModuleInfo
        {
            return moduleInfo as T;
        }
    }
}
