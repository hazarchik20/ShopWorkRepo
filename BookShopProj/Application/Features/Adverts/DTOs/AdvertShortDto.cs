using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Advert
{

    public sealed record AdvertShortDto(
        Guid Id,
        string Title,
        decimal Price,
        bool IsPriceNegotiable,
        string? ImageLink,
        AdvertStatus Status,
        DateTime CreatedAt,
        string Town
    );

}
