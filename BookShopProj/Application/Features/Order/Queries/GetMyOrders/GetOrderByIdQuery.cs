using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrders
{
    using Application.Common.Results;
    using global::Application.Features.Order.DTOs;
    using MediatR;

    public sealed record GetOrderByIdQuery(
        Guid OrderId,
        Guid UserId
    ) : IRequest<Result<OrderDto>>;

}
