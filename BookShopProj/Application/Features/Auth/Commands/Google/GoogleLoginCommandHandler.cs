using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.User;
using Application.Common.Results;
using Application.Features.Auth.DTOs;
using Domain.Entities.User;
using MediatR;

namespace Application.Features.Auth.Commands.Google;

public sealed class GoogleLoginCommandHandler
    : IRequestHandler<GoogleLoginCommand, Result<AuthResultDto>>
{
    private readonly IUserRepository _users;
    private readonly IUserSecurityRepository _securities;
    private readonly IGoogleTokenVerifier _google;
    private readonly ITokenService _tokens;
    private readonly IUnitOfWork _unitOfWork;

    public GoogleLoginCommandHandler(
        IUserRepository users,
        IUserSecurityRepository securities,
        IGoogleTokenVerifier google,
        ITokenService tokens,
        IUnitOfWork unitOfWork)
    {
        _users = users;
        _securities = securities;
        _google = google;
        _tokens = tokens;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthResultDto>> Handle(
        GoogleLoginCommand request,
        CancellationToken cancellationToken)
    {
        var googleUser = await _google.VerifyAsync(request.IdToken, cancellationToken);
        if (googleUser is null)
        {
            return Result<AuthResultDto>.Failure(Error.Unauthorized(
                "Auth.InvalidGoogleToken", "Недійсний або прострочений Google ID Token."));
        }

        var user = await _users.GetByGoogleSubjectAsync(
            googleUser.Subject, cancellationToken);

        if (user is null)
        {
            // Never implicitly take over/link a password account solely by matching email.
            if (googleUser.Email.Length > 320 ||
                await _users.ExistsByEmailAsync(googleUser.Email, cancellationToken))
            {
                return Result<AuthResultDto>.Failure(Error.Conflict(
                    "Auth.GoogleAccountNotLinked",
                    "Email уже використовується. Увійдіть звичайним способом; прив'язку Google додамо окремо."));
            }

            var displayName = string.IsNullOrWhiteSpace(googleUser.Name)
                ? googleUser.Email.Split('@')[0]
                : googleUser.Name.Trim();
            if (displayName.Length > 100) displayName = displayName[..100];

            user = new User
            {
                Id = Guid.NewGuid(),
                Name = displayName,
                Email = googleUser.Email,
                PasswordHash = null,
                GoogleSubject = googleUser.Subject,
                CreatedAt = DateTime.UtcNow
            };

            var security = new UserSecurity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RegistrationIp = request.Ip,
                RegistrationUserAgent = request.UserAgent,
                LastLogin = DateTime.UtcNow
            };

            await _users.AddAsync(user, cancellationToken);
            await _securities.AddAsync(security, cancellationToken);
        }
        else
        {
            var security = await _securities.GetByUserIdAsync(user.Id, cancellationToken);
            if (security is null)
            {
                await _securities.AddAsync(new UserSecurity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RegistrationIp = "Unknown",
                    RegistrationUserAgent = "Unknown",
                    LastLogin = DateTime.UtcNow
                }, cancellationToken);
            }
            else
            {
                security.LastLogin = DateTime.UtcNow;
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _tokens.GenerateAccessToken(user.Id, user.Email);
        return Result<AuthResultDto>.Success(new AuthResultDto(user.Id, token));
    }
}
