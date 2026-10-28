using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.UpdateOrderStatus
{

    using Application.Common.Results;
    using Domain.ValueObjects;
    using MediatR;

    public sealed record UpdateOrderStatusCommand(
        Guid OrderId,
        Guid UserId,
        OrderStatus Status
    ) : IRequest<Result>;

}
