using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects.Advart;
using FluentValidation;


namespace Application.Features.Adverts.Commands.UpdateAdvert
{

    public sealed class UpdateAdvertCommandValidator
        : AbstractValidator<UpdateAdvertCommand>
    {
        public UpdateAdvertCommandValidator()
        {
            RuleFor(x => x.AdvertId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(5000);
            RuleFor(x => x.Town)
                .MaximumLength(150);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Price)
                .Equal(0)
                .When(x => x.AdvertDealType == AdvertDealType.Free)
                .WithMessage("Безкоштовна книга повинна мати ціну 0.");

            RuleFor(x => x.StateOfUse)
                .IsInEnum();

            RuleFor(x => x.AdvertDealType)
                .IsInEnum();

            RuleFor(x => x.SellerType)
                .IsInEnum();

            RuleFor(x => x.Genres)
                .NotNull()
                .Must(x => x is { Count: > 0 })
                .WithMessage("Виберіть хоча б один жанр.");

            RuleFor(x => x.Genres)
                .Must(x => x is not null &&
                    x.All(Enum.IsDefined) &&
                    x.Distinct().Count() == x.Count)
                .When(x => x.Genres is { Count: > 0 })
                .WithMessage("Список жанрів містить помилки.");
        }
    }

}
