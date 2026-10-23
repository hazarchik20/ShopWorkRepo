using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Domain.ValueObjects.Advart;
using MediatR;

namespace Application.Features.Adverts.Commands.UpdateAdvert
{
    public sealed record UpdateAdvertCommand(
        Guid AdvertId,
        Guid UserId,
        string Title,
        string Description,
        decimal Price,
        bool IsPriceNegotiable,
        string? Town,
        AdvertStateOfUse StateOfUse,
        AdvertDealType AdvertDealType,
        AdvertSellerType SellerType,
        List<AdvertGenres> Genres
    ) : IRequest<Result>;

}
