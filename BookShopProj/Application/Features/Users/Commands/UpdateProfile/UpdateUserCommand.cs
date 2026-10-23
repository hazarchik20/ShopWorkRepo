using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using MediatR;

namespace Application.Features.Users.Commands.UpdateProfile
{


    public sealed record UpdateUserCommand(
        Guid UserId,
        string? FirstName,
        string? LastName,
        DateOnly? DateOfBirth,
        string? PhoneNumber,
        string? Town
    ) : IRequest<Result>;

}
