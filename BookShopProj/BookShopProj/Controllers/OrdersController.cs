using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Commands.UpdateOrderStatus;
using Application.Features.Order.Queries.GetMyOrders;
using BookShopProj.Extensions;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookShopProj.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/orders")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _sender.Send(
                new CreateOrderCommand(
                    userId.Value,
                    request.AdvertId),
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _sender.Send(
                new GetMyOrdersQuery(
                    userId.Value,
                    page,
                    pageSize),
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetMySales(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _sender.Send(
                new GetMyOrdersQuery(
                    userId.Value,
                    page,
                    pageSize,
                    Sales: true),
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _sender.Send(
                new GetOrderByIdQuery(
                    id,
                    userId.Value),
                cancellationToken);

            return result.ToActionResult();
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            [FromBody] UpdateOrderStatusRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _sender.Send(
                new UpdateOrderStatusCommand(
                    id,
                    userId.Value,
                    request.Status),
                cancellationToken);

            return result.ToActionResult();
        }
    }

    public sealed record CreateOrderRequest(Guid AdvertId);

    public sealed record UpdateOrderStatusRequest(
        OrderStatus Status);

}
