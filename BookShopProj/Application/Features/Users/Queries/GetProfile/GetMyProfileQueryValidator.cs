using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Queries.GetProfile
{
    public sealed class GetMyProfileQueryValidator
        : AbstractValidator<GetMyProfileQuery>
    {
        public GetMyProfileQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }

}
