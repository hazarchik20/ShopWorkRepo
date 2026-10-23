using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Domain.Entities.Advert;
using MediatR;


namespace Application.Features.Adverts.Queries.GetAdvertById
{

    public sealed record GetAdvertByIdQuery(
        Guid AdvertId
    ) : IRequest<Result<AdvertDetailsDto>>;

}
