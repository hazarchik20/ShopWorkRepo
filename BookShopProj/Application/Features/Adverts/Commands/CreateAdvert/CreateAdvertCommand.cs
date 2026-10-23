using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Storage;
using Application.Common.Results;
using Domain.ValueObjects.Advart;
using MediatR;

namespace Application.Features.Adverts.Commands.CreateAdvert
{


    public sealed record CreateAdvertCommand(
        Guid UserId,
        string Title,
        string Description,
        decimal Price,
        bool IsPriceNegotiable,
        string? Town,
        AdvertStateOfUse StateOfUse,
        AdvertDealType AdvertDealType,
        AdvertSellerType SellerType,
        List<AdvertGenres> Genres,
        List<AdvertUpload> Images
    ) : IRequest<Result<Guid>>;

}
