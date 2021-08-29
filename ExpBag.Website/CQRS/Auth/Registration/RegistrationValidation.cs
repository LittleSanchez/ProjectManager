using FluentValidation;
using ExpBag.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using ExpBag.Domain.CQRSObjects;

namespace ExpBag.Website.CQRS.Auth
{
	public class RegistrationValidation : AbstractValidator<RegistrationCommand>
	{
		public RegistrationValidation()
		{
			RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty().Password();
		}
	}
}
