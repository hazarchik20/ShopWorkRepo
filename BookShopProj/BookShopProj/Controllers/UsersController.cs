using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Queries.GetProfile;
using Application.Features.Users.Queries.GetUserContacts;
using BookShopProj.Contracts.Users;
using BookShopProj.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopProj.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/users")]
    public sealed class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile(
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var query = new GetMyProfileQuery(userId.Value);

            var result = await _sender.Send(
                query,
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpPatch("me")]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var command = new UpdateUserCommand(
                UserId: userId.Value,
                FirstName: request.FirstName,
                LastName: request.LastName,
                DateOfBirth: request.DateOfBirth,
                PhoneNumber: request.PhoneNumber,
                Town: request.Town
            );

            var result = await _sender.Send(
                command,
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("me/contacts")]
        public async Task<IActionResult> GetMyContacts(
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var query = new GetUserContactsQuery(userId.Value);

            var result = await _sender.Send(
                query,
                cancellationToken);

            return result.ToActionResult();
        }
    }
}
