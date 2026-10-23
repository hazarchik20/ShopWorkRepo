using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.User;
using Application.Common.Results;
using MediatR;


namespace Application.Features.Users.Commands.UpdateProfile
{

    public sealed class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "User.NotFound",
                        "Користувача не знайдено."));
            }

            if (request.FirstName is not null)
                user.FirstName = request.FirstName.Trim();

            if (request.LastName is not null)
                user.LastName = request.LastName.Trim();

            if (request.DateOfBirth.HasValue)
                user.DateOfBirth = request.DateOfBirth;

            if (request.PhoneNumber is not null)
                user.PhoneNumber = request.PhoneNumber.Trim();

            if (request.Town is not null)
                user.Town = request.Town.Trim();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
