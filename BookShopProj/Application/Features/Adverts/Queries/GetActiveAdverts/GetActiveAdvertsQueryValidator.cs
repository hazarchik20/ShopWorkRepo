using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Adverts.Queries.GetActiveAdverts
{


    public sealed class GetActiveAdvertsQueryValidator
        : AbstractValidator<GetActiveAdvertsQuery>
    {
        public GetActiveAdvertsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);
        }
    }

}
