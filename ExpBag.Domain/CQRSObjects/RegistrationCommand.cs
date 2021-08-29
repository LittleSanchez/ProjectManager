using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ExpBag.Domain.DTO;

namespace ExpBag.Domain.CQRSObjects
{
	public class RegistrationCommand : IRequest<UserDTO>
	{
		public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

		public string Password { get; set; }
	}
}
