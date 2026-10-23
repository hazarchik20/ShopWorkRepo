using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Domain.Entities.User;
using MediatR;

namespace Application.Features.Users.Queries.GetProfile
{
    public sealed record GetMyProfileQuery(
        Guid UserId
    ) : IRequest<Result<UserProfileDto>>;

}
