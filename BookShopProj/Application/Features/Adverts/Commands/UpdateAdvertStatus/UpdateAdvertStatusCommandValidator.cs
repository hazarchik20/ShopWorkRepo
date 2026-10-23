using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects.Advart;
using FluentValidation;

namespace Application.Features.Adverts.Commands.UpdateAdvertStatus
{

    public sealed class UpdateAdvertStatusCommandValidator
        : AbstractValidator<UpdateAdvertStatusCommand>
    {
        public UpdateAdvertStatusCommandValidator()
        {
            RuleFor(x => x.AdvertId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.Status)
                .Must(status =>
                    status is AdvertStatus.Active
                        or AdvertStatus.Reserved
                        or AdvertStatus.Sold
                        or AdvertStatus.Hidden)
                .WithMessage(
                    "Цей статус не можна встановити самостійно.");
        }
    }

}
