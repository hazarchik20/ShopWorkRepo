using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{

    using FluentValidation;

    public sealed class RegisterUserCommandValidator
        : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(320);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);

            RuleFor(x => x.Ip)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(x => x.UserAgent)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }

}
