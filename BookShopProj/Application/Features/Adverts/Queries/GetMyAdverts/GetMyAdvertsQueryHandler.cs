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


namespace Application.Features.Adverts.Queries.GetMyAdverts
{

    public sealed class GetMyAdvertsQueryHandler
        : IRequestHandler<
            GetMyAdvertsQuery,
            Result<PagedResult<AdvertShortDto>>>
    {
        private readonly IAdvertRepository _advertRepository;

        public GetMyAdvertsQueryHandler(
            IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task<Result<PagedResult<AdvertShortDto>>> Handle(
            GetMyAdvertsQuery request,
            CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetByUserIdAsync(
                request.UserId,
                request.Page,
                request.PageSize,
                cancellationToken);

            var totalCount = await _advertRepository.CountByUserIdAsync(
                request.UserId,
                cancellationToken);

            var items = adverts.Select(x => new AdvertShortDto(
                Id: x.Id,
                Title: x.Title,
                Price: x.Price,
                IsPriceNegotiable: x.IsPriceNegotiable,
                ImageLink: x.ImagesLinks.FirstOrDefault(),
                Status: x.Status,
                CreatedAt: x.CreatedAt,
                StateOfUse: x.StateOfUse,
                Town: x.Town
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
