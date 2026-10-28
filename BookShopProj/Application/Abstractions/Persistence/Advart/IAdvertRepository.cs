using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.Advart
{

    public interface IAdvertRepository : IRepositoryBase<Advert>
    {
        Task<IEnumerable<Advert>> GetByUserIdAsync(
            Guid userId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Advert>> GetActiveAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<bool> IsOwnerAsync(
            Guid advertId,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<int> CountActiveAsync(
            CancellationToken cancellationToken = default);

        Task<int> CountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Advert>> GetLatestCatalogAsync(
          AdvertStateOfUse? stateOfUse,
          int take,
          CancellationToken cancellationToken = default);
        Task<(List<Advert> Items, int TotalCount)> GetCatalogAsync(
          string? search,
          AdvertStateOfUse? stateOfUse,
          string? sort,
          int page,
          int pageSize,
          CancellationToken cancellationToken = default);
    }

}
