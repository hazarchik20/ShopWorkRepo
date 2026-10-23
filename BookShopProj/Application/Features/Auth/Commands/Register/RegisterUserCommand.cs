using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    using Application.Common.Results;
    using Application.Features.Auth.DTOs;
    using MediatR;

    public sealed record RegisterUserCommand(
        string Name,
        string Email,
        string Password,
        string Ip,
        string UserAgent
    ) : IRequest<Result<AuthResultDto>>;

}
