using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.CreateOrder
{
    using Application.Common.Results;
    using global::Application.Features.Order.DTOs;
    using MediatR;

    public sealed record CreateOrderCommand(
        Guid BuyerId,
        Guid AdvertId
    ) : IRequest<Result<OrderDto>>;

}
