
using Application.Abstractions.Persistence.Advart;
using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Advart;

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
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Advert>> GetByUserIdAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                x.Status != AdvertStatus.Deleted)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Advert>> GetActiveAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => x.Status == AdvertStatus.Active)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsOwnerAsync(
        Guid advertId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(
            x => x.Id == advertId &&
                 x.UserId == userId &&
                 x.Status != AdvertStatus.Deleted,
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

    // Каталог: пошук, фільтрація, сортування, пагінація
    public async Task<(List<Advert> Items, int TotalCount)> GetCatalogAsync(
        string? search,
        AdvertStateOfUse? stateOfUse,
        string? sort,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .ApplyCatalogFilters(search, stateOfUse);

        var totalCount = await query.CountAsync(
            cancellationToken);

        var items = await query
            .ApplyCatalogSort(sort)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    // Добірки для головної сторінки
    public async Task<IEnumerable<Advert>> GetLatestCatalogAsync(
        AdvertStateOfUse? stateOfUse,
        int take,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .ApplyCatalogFilters(null, stateOfUse)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(take)
            .ToListAsync(cancellationToken);
    }


}
