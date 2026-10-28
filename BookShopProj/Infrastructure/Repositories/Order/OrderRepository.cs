using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Order
{

    using Application.Abstractions.Persistence.Order;
    using Domain.Entities.Order;
    using Domain.ValueObjects;
    using Domain.ValueObjects.Advart;
    using Microsoft.EntityFrameworkCore;


    public sealed class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Order?> CreateAsync(
            Guid buyerId,
            Guid advertId,
            CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync(cancellationToken);

            var advert = await _db.Adverts
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == advertId,
                    cancellationToken);

            if (advert is null ||
                advert.Status != AdvertStatus.Active ||
                advert.UserId == buyerId)
            {
                return null;
            }

            // Атомарно резервуємо книгу.
            // Лише один покупець зможе змінити Active на Reserved.
            var affected = await _db.Adverts
                .Where(x =>
                    x.Id == advertId &&
                    x.Status == AdvertStatus.Active)
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(
                            x => x.Status,
                            AdvertStatus.Reserved)
                        .SetProperty(
                            x => x.UpdatedAt,
                            DateTime.UtcNow),
                    cancellationToken);

            if (affected != 1)
                return null;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                BuyerId = buyerId,
                SellerId = advert.UserId,
                AdvertId = advert.Id,
                BookTitle = advert.Title,
                ImageLink = advert.ImagesLinks.FirstOrDefault(),
                Price = advert.Price,
                Status = OrderStatus.Processing,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _db.Orders.AddAsync(order, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return order;
        }

        public Task<Order?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return _db.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public Task<List<Order>> GetByBuyerIdAsync(
            Guid buyerId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return _db.Orders
                .AsNoTracking()
                .Where(x => x.BuyerId == buyerId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Number)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public Task<List<Order>> GetBySellerIdAsync(
            Guid sellerId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return _db.Orders
                .AsNoTracking()
                .Where(x => x.SellerId == sellerId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Number)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public Task<int> CountByBuyerIdAsync(
            Guid buyerId,
            CancellationToken cancellationToken = default)
        {
            return _db.Orders.CountAsync(
                x => x.BuyerId == buyerId,
                cancellationToken);
        }

        public Task<int> CountBySellerIdAsync(
            Guid sellerId,
            CancellationToken cancellationToken = default)
        {
            return _db.Orders.CountAsync(
                x => x.SellerId == sellerId,
                cancellationToken);
        }

        public async Task<bool> ChangeStatusAsync(
            Guid orderId,
            Guid userId,
            OrderStatus status,
            CancellationToken cancellationToken = default)
        {
            await using var transaction =
                await _db.Database.BeginTransactionAsync(cancellationToken);

            var order = await _db.Orders
                .FirstOrDefaultAsync(
                    x => x.Id == orderId,
                    cancellationToken);

            if (order is null)
                return false;

            // Покупець може скасувати замовлення до відправлення.
            // Продавець керує виконанням замовлення.
            var allowed = order.Status switch
            {
                OrderStatus.Processing =>
                    (status == OrderStatus.Cancelled &&
                     order.BuyerId == userId) ||
                    (status == OrderStatus.Confirmed &&
                     order.SellerId == userId),

                OrderStatus.Confirmed =>
                    status == OrderStatus.Shipped &&
                    order.SellerId == userId,

                OrderStatus.Shipped =>
                    status == OrderStatus.Delivered &&
                    order.SellerId == userId,

                _ => false
            };

            if (!allowed)
                return false;

            var nextAdvertStatus =
                status == OrderStatus.Cancelled
                    ? AdvertStatus.Active
                    : status == OrderStatus.Delivered
                        ? AdvertStatus.Sold
                        : AdvertStatus.Reserved;

            var changedAdvert = await _db.Adverts
                .Where(x =>
                    x.Id == order.AdvertId &&
                    x.Status == AdvertStatus.Reserved)
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(x => x.Status, nextAdvertStatus)
                        .SetProperty(x => x.UpdatedAt, DateTime.UtcNow),
                    cancellationToken);

            if (changedAdvert != 1)
                return false;

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }
    }

}
