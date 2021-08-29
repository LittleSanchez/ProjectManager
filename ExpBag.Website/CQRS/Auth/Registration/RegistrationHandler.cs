using ExpBag.Application.Exceptions;
using ExpBag.Application.Interfaces;
using ExpBag.Domain;
using ExpBag.EFData;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExpBag.Website.CQRS.Auth;
using ExpBag.Domain.DTO;
using ExpBag.Domain.CQRSObjects;

namespace ExpBag.Website.CQRS.Auth
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, UserDTO>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IJwtGenerator _jwtGenerator;
		private readonly DataContext _context;

		public RegistrationHandler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
		{
			_context = context;
			_userManager = userManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<UserDTO> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
			{
				throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exist" });
			}

			/*if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
			{
				throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exist" });
			}*/

			var user = new AppUser
			{
				DisplayName = request.DisplayName,
				Email = request.Email,
				UserName = request.UserName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				return new UserDTO
				{
					DisplayName = user.DisplayName,
					Token = _jwtGenerator.CreateToken(user),
					UserName = user.UserName,
					Image = null
				};
			}

			throw new Exception("Client creation failed");
		}
	}
}
