using Application.Abstractions.Persistence.Advart;
using Application.Common.Results;
using Application.Features.Adverts.DTOs;
using Application.Features.Adverts.Mappers;
using Domain.ValueObjects.Advart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Queries.GetCatalogSectionsQuery
{
    public sealed class GetCatalogSectionsQueryHandler
        : IRequestHandler<GetCatalogSectionsQuery, Result<IReadOnlyList<CatalogSectionDto>>>
    {
        private readonly IAdvertRepository _repository;
        public GetCatalogSectionsQueryHandler(IAdvertRepository repository) => _repository = repository;

        public async Task<Result<IReadOnlyList<CatalogSectionDto>>> Handle(
            GetCatalogSectionsQuery request, CancellationToken ct)
        {
            // Both sections have honest, reproducible database-backed criteria.
            var latest = await _repository.GetLatestCatalogAsync(null, 12, ct);
            var used = await _repository.GetLatestCatalogAsync(AdvertStateOfUse.Used, 12, ct);

            IReadOnlyList<CatalogSectionDto> sections = new List<CatalogSectionDto>
        {
            new("latest", "Нещодавно додані",
                latest.Select(AdvertCatalogMapper.ToShortDto).ToList()),
            new("used", "Вживані книги",
                used.Select(AdvertCatalogMapper.ToShortDto).ToList())
        }.Where(s => s.Books.Count > 0).ToList();

            return Result<IReadOnlyList<CatalogSectionDto>>.Success(sections);
        }
    }
}
