using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.Advart;
using Application.Common.Pagination;
using Application.Common.Results;
using Domain.Entities.Advert;
using MediatR;

namespace Application.Features.Adverts.Queries.GetActiveAdverts
{


    public sealed class GetActiveAdvertsQueryHandler
        : IRequestHandler<
            GetActiveAdvertsQuery,
            Result<PagedResult<AdvertShortDto>>>
    {
        private readonly IAdvertRepository _advertRepository;

        public GetActiveAdvertsQueryHandler(
            IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task<Result<PagedResult<AdvertShortDto>>> Handle(
            GetActiveAdvertsQuery request,
            CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetActiveAsync(
                request.Page,
                request.PageSize,
                cancellationToken);

            var totalCount = await _advertRepository.CountActiveAsync(
                cancellationToken);

            var items = adverts.Select(x => new AdvertShortDto(
                x.Id,
                x.Title,
                x.Price,
                x.IsPriceNegotiable,
                x.ImagesLinks.FirstOrDefault(),
                x.Status,
                x.CreatedAt,
                x.Town
            )).ToList();

            var result = new PagedResult<AdvertShortDto>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );

            return Result<PagedResult<AdvertShortDto>>.Success(result);
        }
    }

}
