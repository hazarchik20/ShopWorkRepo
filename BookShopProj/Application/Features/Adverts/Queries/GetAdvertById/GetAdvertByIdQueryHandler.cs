using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.Advart;
using Application.Common.Results;
using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using MediatR;

namespace Application.Features.Adverts.Queries.GetAdvertById
{


    public sealed class GetAdvertByIdQueryHandler
        : IRequestHandler<GetAdvertByIdQuery, Result<AdvertDetailsDto>>
    {
        private readonly IAdvertRepository _advertRepository;

        public GetAdvertByIdQueryHandler(
            IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task<Result<AdvertDetailsDto>> Handle(
            GetAdvertByIdQuery request,
            CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetByIdAsync(
                request.AdvertId,
                cancellationToken);

            if (advert is null ||
                advert.Status != AdvertStatus.Active)
            {
                return Result<AdvertDetailsDto>.Failure(
                    Error.NotFound(
                        "Advert.NotFound",
                        "Оголошення не знайдено."));
            }

            var dto = new AdvertDetailsDto(
                advert.Id,
                advert.UserId,
                advert.Title,
                advert.Description,
                advert.Price,
                advert.IsPriceNegotiable,
                advert.StateOfUse,
                advert.AdvertDealType,
                advert.SellerType,
                advert.Status,
                advert.Genres.ToList(),
                advert.ImagesLinks.ToList(),
                advert.CreatedAt,
                advert.UpdatedAt
            );

            return Result<AdvertDetailsDto>.Success(dto);
        }
    }

}
