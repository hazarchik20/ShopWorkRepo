using Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public abstract class RepositoryBase<T> : IRepositoryBase<T>
     where T : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(
                new object[] { id },
                cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .OrderBy(x => EF.Property<Guid>(x, "Id"))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null)
            {
                _dbSet.Remove(entity);
            }
        }
    }

}
