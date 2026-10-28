using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrders
{
    using Application.Abstractions.Persistence.Order;
    using Application.Common.Results;
    using global::Application.Common.Pagination;
    using global::Application.Features.Order.DTOs;
    using global::Application.Features.Order.Mappers;
    using MediatR;
    public sealed class GetMyOrdersQueryHandler
        : IRequestHandler<
            GetMyOrdersQuery,
            Result<PagedResult<OrderDto>>>
    {
        private readonly IOrderRepository _repository;

        public GetMyOrdersQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<OrderDto>>> Handle(
            GetMyOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var page = Math.Max(request.Page, 1);
            var pageSize = Math.Clamp(request.PageSize, 1, 50);

            var orders = request.Sales
                ? await _repository.GetBySellerIdAsync(
                    request.UserId, page, pageSize, cancellationToken)
                : await _repository.GetByBuyerIdAsync(
                    request.UserId, page, pageSize, cancellationToken);

            var totalCount = request.Sales
                ? await _repository.CountBySellerIdAsync(
                    request.UserId, cancellationToken)
                : await _repository.CountByBuyerIdAsync(
                    request.UserId, cancellationToken);

            var result = new PagedResult<OrderDto>(
                orders.Select(OrderMapper.ToDto).ToList(),
                page,
                pageSize,
                totalCount
            );

            return Result<PagedResult<OrderDto>>.Success(result);
        }
    }

}
