using ExpBag.Application.Constans;
using ExpBag.Application.Interfaces;
using ExpBag.Domain.DTO;
using ExpBag.UI.Abstractions;
using ExpBag.UI.Extentions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.UI.Services
{
    public class ModuleService : IModuleService
    {
        private IFetchService fetchService;


        private string Url = Path.Combine(NetworkConfig.ServerUrl, NetworkConfig.ApiModulesPath);


        public ModuleService(IFetchService _fetchService)
        {
            fetchService = _fetchService;
        }
       

        public async Task<List<ExpModuleDTO>> GetExpModulesAsync()
        {
            var list = (await fetchService.Fetch<object, List<ExpModuleDTO>>(Url, "GET", null, (opt) => {
                opt.Authorize();
            }));
            return list;
        }

        public async Task<ExpModuleDTO> AddExpModuleAsync(ExpModuleDTO moduleDTO)
        {
            var resultModuleDto = await fetchService.Fetch<ExpModuleDTO, ExpModuleDTO>(Url, "POST", moduleDTO, (opt) =>
            {
                opt.Authorize();
            });
            return resultModuleDto;
        }

        public async Task<ExpModuleDTO> UpdateExpModuleAsync(int id, ExpModuleDTO moduleDTO)
        {
            return await fetchService.Fetch<ExpModuleDTO, ExpModuleDTO>(Path.Combine(Url, moduleDTO.Id.ToString()), "PUT", moduleDTO, (opt) =>
            {
                opt.Authorize();
            });
        }

        public async Task DeleteExpModuleAsync(int id)
        {
            await fetchService.Fetch<ExpModuleDTO, ExpModuleDTO>(Path.Combine(Url, id.ToString()), "DELETE", null, (opt) =>
            {
                opt.Authorize();
            });
        }

        public async Task<ExpModuleDTO> UploadFiles(ExpModuleDTO moduleDTO, string moduleDirectory)
        {
            return await fetchService.FetchFilesAsync<ExpModuleDTO>(Path.Combine(Url, "upload", moduleDTO.Id.ToString()),
                moduleDTO.IncludedFiles.Select(x => new FormFile
                {
                    FilePath = Path.Combine(moduleDirectory, x.FilePath),
                    FormElementName = $"{x.Id}"
                }),
            opt => opt.Authorize());
        }
    }
}
