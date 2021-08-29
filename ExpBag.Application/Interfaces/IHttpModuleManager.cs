using ExpBag.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.Interfaces
{
    public interface IHttpModuleManager
    {
        Task SendModule(ExpModuleDTO module);

    }
}
