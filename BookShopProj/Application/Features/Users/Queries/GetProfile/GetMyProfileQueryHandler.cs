using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.User;
using Application.Common.Results;
using Domain.Entities.User;
using MediatR;


namespace Application.Features.Users.Queries.GetProfile
{

    public sealed class GetMyProfileQueryHandler
        : IRequestHandler<GetMyProfileQuery, Result<UserProfileDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetMyProfileQueryHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserProfileDto>> Handle(
            GetMyProfileQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
            {
                return Result<UserProfileDto>.Failure(
                    Error.NotFound(
                        "User.NotFound",
                        "Користувача не знайдено."));
            }

            var dto = new UserProfileDto(
                user.Id,
                user.Name,
                user.Email,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.PhoneNumber,
                user.Town,
                user.CreatedAt
            );

            return Result<UserProfileDto>.Success(dto);
        }
    }

}
