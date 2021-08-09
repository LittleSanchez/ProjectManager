using MediatR;
using ExpBag.Website.CQRS.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpBag.Website.CQRS.Auth
{
	public class RegistrationCommand : IRequest<UserViewModel>
	{
		public string DisplayName { get; set; }

		//public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}
