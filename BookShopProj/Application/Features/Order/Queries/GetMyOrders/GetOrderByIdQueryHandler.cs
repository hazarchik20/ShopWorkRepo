using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrders
{
    using Application.Abstractions.Persistence.Order;
    using Application.Common.Results;
    using global::Application.Features.Order.DTOs;
    using global::Application.Features.Order.Mappers;
    using MediatR;

    public sealed class GetOrderByIdQueryHandler
        : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDto>> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(
                request.OrderId,
                cancellationToken);

            if (order is null ||
                (order.BuyerId != request.UserId &&
                 order.SellerId != request.UserId))
            {
                return Result<OrderDto>.Failure(
                    Error.NotFound(
                        "Order.NotFound",
                        "Замовлення не знайдено."));
            }

            return Result<OrderDto>.Success(
                OrderMapper.ToDto(order));
        }
    }

}
