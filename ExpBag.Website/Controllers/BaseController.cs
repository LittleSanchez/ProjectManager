using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExpBag.EFData;
using AutoMapper;

namespace ExpBag.Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private DataContext _context;
        private IMapper _mapper;

        protected IMediator Mediator => _mediator ??
            (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected DataContext Context => _context ??
            (_context = HttpContext.RequestServices.GetService<DataContext>());

        protected IMapper Mapper => _mapper ??
            (_mapper = HttpContext.RequestServices.GetService<Mapper>());

        
    }
}
