using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities.Order
{

    public sealed class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public long Number { get; set; }

        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid AdvertId { get; set; }

        public string BookTitle { get; set; } = null!;
        public string? ImageLink { get; set; }
        public decimal Price { get; set; }

        public OrderStatus Status { get; set; } =
            OrderStatus.Processing;

        public DateTime CreatedAt { get; set; } =
            DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } =
            DateTime.UtcNow;

        public string OrderNumber =>
            $"RD-{CreatedAt:yyyy}-{Number:D6}";
    }

}
