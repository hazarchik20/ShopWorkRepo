using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Queries.GetUserContacts
{
    public sealed class GetUserContactsQueryValidator
        : AbstractValidator<GetUserContactsQuery>
    {
        public GetUserContactsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId не може бути порожнім.");
        }
    }

}
