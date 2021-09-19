using ExpBag.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.Abstractions
{
    public interface IModuleService
    {
        public Task<ExpModuleDTO> AddExpModuleAsync(ExpModuleDTO moduleDTO);
        public Task<List<ExpModuleDTO>> GetExpModulesAsync();
        public Task<ExpModuleDTO> UpdateExpModuleAsync(int id, ExpModuleDTO moduleDTO);
        public Task DeleteExpModuleAsync(int id);
        public Task<ExpModuleDTO> UploadFiles(ExpModuleDTO moduleDTO, string moduleDirectory);
    }
}
