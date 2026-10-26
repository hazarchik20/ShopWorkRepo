using Domain.Entities.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Mappers
{
    public class AdvertCatalogMapper
    {
        public static AdvertShortDto ToShortDto(Advert advert) => new(
            advert.Id,
            advert.Title,
            advert.Price,
            advert.IsPriceNegotiable,
            advert.ImagesLinks.FirstOrDefault(),
            advert.Status,
            advert.CreatedAt,
            advert.StateOfUse,
            advert.Town
        );
    }
}
