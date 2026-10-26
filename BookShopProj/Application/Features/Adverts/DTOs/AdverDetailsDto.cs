using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Advert
{

    public sealed record AdvertDetailsDto(
        Guid Id,
        Guid UserId,
        string Title,
        string Description,
        string? Town ,
        decimal Price,
        bool IsPriceNegotiable,
        AdvertStateOfUse StateOfUse,
        AdvertDealType AdvertDealType,
        AdvertSellerType SellerType,
        AdvertStatus Status,
        List<AdvertGenres> Genres,
        List<string> ImagesLinks,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

}
