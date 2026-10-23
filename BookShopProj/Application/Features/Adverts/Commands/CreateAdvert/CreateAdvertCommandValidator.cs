using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Commands.CreateAdvert
{
    using Domain.ValueObjects.Advart;
    using FluentValidation;

    public sealed class CreateAdvertCommandValidator
        : AbstractValidator<CreateAdvertCommand>
    {
        public CreateAdvertCommandValidator()
        {
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

            RuleFor(x => x.Price)
                .Equal(0)
                .When(x => x.AdvertDealType == AdvertDealType.Free)
                .WithMessage("Безкоштовна книга повинна мати ціну 0.");

            RuleFor(x => x.Images)
                .NotNull()
                .Must(x => x is { Count: >= 1 and <= 10 })
                .WithMessage("Потрібно завантажити від 1 до 10 фото.");

            RuleForEach(x => x.Images)
                .ChildRules(image =>
                {
                    image.RuleFor(x => x.Length)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(5 * 1024 * 1024);

                    image.RuleFor(x => x.ContentType)
                        .Must(x => x is
                            "image/jpeg" or
                            "image/png" or
                            "image/webp")
                        .WithMessage("Дозволені JPEG, PNG та WEBP.");

                    image.RuleFor(x => x.Content)
                        .NotNull()
                        .Must(x => x.CanRead)
                        .When(x => x.Content is not null);
                })
                .When(x => x.Images is not null);
        }
    }

}
