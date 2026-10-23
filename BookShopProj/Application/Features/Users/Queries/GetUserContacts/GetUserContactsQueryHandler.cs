using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.User;
using Application.Common.Results;
using Domain.Entities.User;
using MediatR;

namespace Application.Features.Users.Queries.GetUserContacts
{
    public sealed class GetUserContactsQueryHandler
        : IRequestHandler<GetUserContactsQuery, Result<UserContactsDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserContactsQueryHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserContactsDto>> Handle(
            GetUserContactsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
            {
                return Result<UserContactsDto>.Failure(
                    Error.NotFound(
                        "User.NotFound",
                        "Користувача не знайдено."));
            }

            var dto = new UserContactsDto(
                user.Id,
                user.Name,
                user.Email,
                user.Town,
                user.PhoneNumber
            );

            return Result<UserContactsDto>.Success(dto);
        }
    }

}
