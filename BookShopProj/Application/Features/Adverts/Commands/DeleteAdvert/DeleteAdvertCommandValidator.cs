using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Commands.DeleteAdvert
{

    using FluentValidation;

    public sealed class DeleteAdvertCommandValidator
        : AbstractValidator<DeleteAdvertCommand>
    {
        public DeleteAdvertCommandValidator()
        {
            RuleFor(x => x.AdvertId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }

}
