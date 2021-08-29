using AutoMapper;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.AutoMapper
{
    public static class MapperProfile
    {
        public static void Create(IMapperConfigurationExpression cfg)
        {
            //expmodule - dto
            cfg.CreateMap<ExpModule, ExpModuleDTO>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));
            //dto - expmodule
            cfg.CreateMap<ExpModuleDTO, ExpModule>()
                .ForMember(x => x.User, opt => opt.Ignore());



            //expmodulefile - dto
            cfg.CreateMap<ExpModuleFile, ExpModuleFileDTO>()
                .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.Module.ModuleName));

            //dto - expmodulefile
            cfg.CreateMap<ExpModuleFileDTO, ExpModuleFile>()
                .ForMember(x => x.Module, opt => opt.Ignore());



            //expmoduleinfo - dto
            cfg.CreateMap<ExpModuleExtention, ExpModuleExtentionDTO>()
                .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.Module.ModuleName));
            //dto - expmoduleinfo
            cfg.CreateMap<ExpModuleExtentionDTO, ExpModuleExtention>()
                .ForMember(x => x.Module, opt => opt.Ignore());
            
        }
    }
}
