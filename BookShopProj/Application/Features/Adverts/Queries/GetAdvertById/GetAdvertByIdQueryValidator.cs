using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Queries.GetAdvertById
{

    using FluentValidation;

    public sealed class GetAdvertByIdQueryValidator
        : AbstractValidator<GetAdvertByIdQuery>
    {
        public GetAdvertByIdQueryValidator()
        {
            RuleFor(x => x.AdvertId)
                .NotEmpty();
        }
    }

}
