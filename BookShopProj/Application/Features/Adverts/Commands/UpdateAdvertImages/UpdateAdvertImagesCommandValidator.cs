using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Commands.UpdateAdvertImages
{

    using FluentValidation;

    public sealed class UpdateAdvertImagesCommandValidator
        : AbstractValidator<UpdateAdvertImagesCommand>
    {
        public UpdateAdvertImagesCommandValidator()
        {
            RuleFor(x => x.AdvertId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.KeepImages)
                .NotNull();

            RuleFor(x => x.NewImages)
                .NotNull();

            RuleFor(x => x)
                .Must(x =>
                    x.KeepImages is not null &&
                    x.NewImages is not null &&
                    x.KeepImages.Count + x.NewImages.Count is >= 1 and <= 10)
                .WithMessage("Оголошення повинно мати від 1 до 10 фотографій.");

            RuleFor(x => x.KeepImages)
                .Must(x => x is not null &&
                    x.All(path => !string.IsNullOrWhiteSpace(path)) &&
                    x.Distinct(StringComparer.Ordinal).Count() == x.Count)
                .When(x => x.KeepImages is not null)
                .WithMessage("Список фотографій містить дублікати або порожні шляхи.");

            RuleForEach(x => x.NewImages)
                .ChildRules(image =>
                {
                    image.RuleFor(x => x.Length)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(5 * 1024 * 1024);

                    image.RuleFor(x => x.ContentType)
                        .Must(x => x is
                            "image/jpeg" or
                            "image/png" or
                            "image/webp");

                    image.RuleFor(x => x.Content)
                        .NotNull()
                        .Must(x => x.CanRead)
                        .When(x => x.Content is not null);
                })
                .When(x => x.NewImages is not null);
        }
    }

}
