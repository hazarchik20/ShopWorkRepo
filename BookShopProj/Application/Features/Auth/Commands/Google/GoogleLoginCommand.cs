using Application.Common.Results;
using Application.Features.Auth.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands.Google;

public sealed record GoogleLoginCommand(
    string IdToken,
    string Ip,
    string UserAgent) : IRequest<Result<AuthResultDto>>;
