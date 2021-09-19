using ExpBag.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain
{
    public class ExpModuleStored : ExpModuleDTO
    {
        public bool IsLoaded { get; set; } = false;
        public bool IsDownloaded { get; set; } = false;
    }
}
