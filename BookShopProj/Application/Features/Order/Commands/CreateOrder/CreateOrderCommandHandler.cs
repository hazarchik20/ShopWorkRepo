using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.CreateOrder
{

    using Application.Abstractions.Persistence.Order;
    using Application.Common.Results;
    using global::Application.Features.Order.DTOs;
    using global::Application.Features.Order.Mappers;
    using MediatR;
    public sealed class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDto>> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _repository.CreateAsync(
                request.BuyerId,
                request.AdvertId,
                cancellationToken);

            if (order is null)
            {
                return Result<OrderDto>.Failure(
                    Error.Conflict(
                        "Order.CannotCreate",
                        "Книга недоступна для замовлення."));
            }

            return Result<OrderDto>.Success(
                OrderMapper.ToDto(order));
        }
    }

}
