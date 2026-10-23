using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Queries.GetMyAdverts
{

    using FluentValidation;

    public sealed class GetMyAdvertsQueryValidator
        : AbstractValidator<GetMyAdvertsQuery>
    {
        public GetMyAdvertsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);
        }
    }

}
