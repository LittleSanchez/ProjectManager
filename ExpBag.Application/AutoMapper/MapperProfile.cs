using AutoMapper;
using ExpBag.Domain;
using ExpBag.Domain.DTO;
using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.AutoMapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {

            //expmodule - dto
            CreateMap<ExpModule, ExpModuleDTO>()
                    //.ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                    ;
            //dto - expmodule
            CreateMap<ExpModuleDTO, ExpModule>()
                .ForMember(x => x.User, opt => opt.Ignore())
                ;



            //expmodulefile - dto
            CreateMap<ExpModuleFile, ExpModuleFileDTO>()
                    .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.Module.ModuleName))
                    ;

            //dto - expmodulefile
            CreateMap<ExpModuleFileDTO, ExpModuleFile>()
                .ForMember(x => x.Module, opt => opt.Ignore())
                ;



            //expmoduleinfo - dto
            CreateMap<ExpModuleExtention, ExpModuleExtentionDTO>()
                    .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.Module.ModuleName))
                    ;
            //dto - expmoduleinfo
            CreateMap<ExpModuleExtentionDTO, ExpModuleExtention>()
                .ForMember(x => x.Module, opt => opt.Ignore())
                ;


        }
    }
}
