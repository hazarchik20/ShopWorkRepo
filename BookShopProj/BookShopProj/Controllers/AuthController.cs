using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using BookShopProj.Contracts.Auth;
using BookShopProj.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopProj.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?
                .ToString() ?? "Unknown";

            var userAgent = Request.Headers.UserAgent.ToString();

            if (string.IsNullOrWhiteSpace(userAgent))
            {
                userAgent = "Unknown";
            }

            var command = new RegisterUserCommand(
                Name: request.Name,
                Email: request.Email,
                Password: request.Password,
                Ip: ip,
                UserAgent: userAgent
            );

            var result = await _sender.Send(
                command,
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(
                Email: request.Email,
                Password: request.Password
            );

            var result = await _sender.Send(
                command,
                cancellationToken);

            return result.ToActionResult();
        }
    }
}
