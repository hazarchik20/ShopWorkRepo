using Domain.Entities.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.DTOs
{
    public sealed record CatalogSectionDto(
        string Id,
        string Title,
        IReadOnlyList<AdvertShortDto> Books);

}
