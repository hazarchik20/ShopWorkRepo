using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Application.Features.Auth.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public sealed record LoginUserCommand(
        string Email,
        string Password
    ) : IRequest<Result<AuthResultDto>>;

}
