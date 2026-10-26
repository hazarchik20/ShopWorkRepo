using FluentValidation;

namespace Application.Features.Auth.Commands.Google;

public sealed class GoogleLoginCommandValidator : AbstractValidator<GoogleLoginCommand>
{
    public GoogleLoginCommandValidator()
    {
        RuleFor(x => x.IdToken).NotEmpty().MaximumLength(10_000);
        RuleFor(x => x.Ip).NotEmpty().MaximumLength(45);
        RuleFor(x => x.UserAgent).NotEmpty().MaximumLength(1000);
    }
}
