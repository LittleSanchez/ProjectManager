using FluentValidation;
using ProjectManager.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Website.CQRS.Auth
{
	public class RegistrationValidation : AbstractValidator<RegistrationCommand>
	{
		public RegistrationValidation()
		{
			RuleFor(x => x.DisplayName).NotEmpty();
			//RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty().Password();
		}
	}
}
