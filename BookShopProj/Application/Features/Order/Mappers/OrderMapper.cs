using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Mappers
{
    using Domain.Entities.Order;
    using global::Application.Features.Order.DTOs;
    public static class OrderMapper
    {
        public static OrderDto ToDto(Order order) => new(
            order.Id,
            order.OrderNumber,
            order.AdvertId,
            order.BookTitle,
            order.ImageLink,
            order.Price,
            order.Status,
            order.CreatedAt
        );
    }

}
