using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateProfile
{


    public sealed class UpdateUserCommandValidator
        : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .When(x => x.FirstName is not null);

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .When(x => x.LastName is not null);

            RuleFor(x => x.DateOfBirth)
                .Must(date => date <= DateOnly.FromDateTime(DateTime.UtcNow))
                .When(x => x.DateOfBirth.HasValue)
                .WithMessage("Дата народження не може бути в майбутньому.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Некоректний номер телефону.");

            RuleFor(x => x.Town)
                .MaximumLength(150)
                .When(x => x.Town is not null);
        }
    }

}
