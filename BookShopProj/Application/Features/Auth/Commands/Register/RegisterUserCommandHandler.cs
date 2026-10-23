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

namespace Application.Features.Auth.Commands.Register
{


    public sealed class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Result<AuthResultDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSecurityRepository _securityRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
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
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            if (await _userRepository.ExistsByEmailAsync(
                email, cancellationToken))
            {
                return Result<AuthResultDto>.Failure(
                    Error.Conflict(
                        "User.EmailAlreadyExists",
                        "Користувач із таким email вже існує."));
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Email = email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            var security = new UserSecurity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RegistrationIp = request.Ip,
                RegistrationUserAgent = request.UserAgent
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _securityRepository.AddAsync(
                security, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var token = _tokenService.GenerateAccessToken(user.Id,user.Email);

            return Result<AuthResultDto>.Success(
                new AuthResultDto(user.Id, token));
        }
    }

}
