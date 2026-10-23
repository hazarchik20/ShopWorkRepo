using Application.Abstractions.Persistence.User;
using Domain.Entities.Advert;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.Advart;
using Domain.ValueObjects.Advart;


namespace Infrastructure.Repositories.Advart
{


    using Microsoft.EntityFrameworkCore;

    public sealed class AdvertRepository
        : RepositoryBase<Advert>, IAdvertRepository
    {
        public AdvertRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Advert?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }


        public async Task<IEnumerable<Advert>> GetByUserIdAsync(
            Guid userId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(x =>
                    x.UserId == userId &&
                    x.Status != AdvertStatus.Deleted)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<Advert>> GetActiveAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(x => x.Status == AdvertStatus.Active)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsOwnerAsync(
            Guid advertId,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(
                x => x.Id == advertId &&
                     x.UserId == userId,
                cancellationToken);
        }

        public async Task<int> CountActiveAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(
                x => x.Status == AdvertStatus.Active,
                cancellationToken);
        }

        public async Task<int> CountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(
                x => x.UserId == userId &&
                     x.Status != AdvertStatus.Deleted,
                cancellationToken);
        }


    }
}
