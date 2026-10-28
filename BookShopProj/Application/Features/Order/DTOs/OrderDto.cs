using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.DTOs
{
    public sealed record OrderDto(
        Guid Id,
        string OrderNumber,
        Guid AdvertId,
        string BookTitle,
        string? ImageLink,
        decimal Price,
        OrderStatus Status,
        DateTime CreatedAt
    );

}
