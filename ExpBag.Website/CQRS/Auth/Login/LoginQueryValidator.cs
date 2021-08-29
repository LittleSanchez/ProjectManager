using ExpBag.Domain.CQRSObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpBag.Website.CQRS.Auth.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Пошта не може бути порожньою");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль не може бути порожнім");
        }
    }
}
