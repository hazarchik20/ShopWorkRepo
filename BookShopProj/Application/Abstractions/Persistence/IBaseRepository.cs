using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence
{

    public interface IRepositoryBase<T> where T : class
    {
        Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }

}
