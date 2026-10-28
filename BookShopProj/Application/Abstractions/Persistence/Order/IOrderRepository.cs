using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.Order
{
    using Domain.Entities.Order;
    public interface IOrderRepository
    {
        Task<Order?> CreateAsync(
            Guid buyerId,
            Guid advertId,
            CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<Order>> GetByBuyerIdAsync(
            Guid buyerId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<List<Order>> GetBySellerIdAsync(
            Guid sellerId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<int> CountByBuyerIdAsync(
            Guid buyerId,
            CancellationToken cancellationToken = default);

        Task<int> CountBySellerIdAsync(
            Guid sellerId,
            CancellationToken cancellationToken = default);

        Task<bool> ChangeStatusAsync(
            Guid orderId,
            Guid userId,
            Domain.ValueObjects.OrderStatus status,
            CancellationToken cancellationToken = default);
    }

}
