using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.User;
using Application.Common.Results;
using Application.Features.Auth.DTOs;
using Domain.Entities.User;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{

    public sealed class LoginUserCommandHandler
        : IRequestHandler<LoginUserCommand, Result<AuthResultDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSecurityRepository _securityRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IUserSecurityRepository securityRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _securityRepository = securityRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthResultDto>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            // Find user
            var user = await _userRepository.GetByEmailAsync(
                email,
                cancellationToken);

            if (user is null)
            {
                return Result<AuthResultDto>.Failure(
                    Error.Unauthorized(
                        "Auth.InvalidCredentials",
                        "Неправильний email або пароль."));
            }

            // Verify password
            var isValidPassword = _passwordHasher.Verify(
                request.Password,
                user.PasswordHash);

            if (!isValidPassword)
            {
                return Result<AuthResultDto>.Failure(
                    Error.Unauthorized(
                        "Auth.InvalidCredentials",
                        "Неправильний email або пароль."));
            }

            // Update last login
            var security = await _securityRepository.GetByUserIdAsync(
                user.Id,
                cancellationToken);

            if (security is not null)
            {
                security.LastLogin = DateTime.UtcNow;
            }
            else
            {
                security = new UserSecurity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RegistrationIp = "Unknown",
                    RegistrationUserAgent = "Unknown",
                    LastLogin = DateTime.UtcNow
                };

                await _securityRepository.AddAsync(
                    security,
                    cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Generate JWT
            var token = _tokenService.GenerateAccessToken(user.Id, user.Email);

            return Result<AuthResultDto>.Success(
                new AuthResultDto(user.Id, token));
        }
    }

}
