using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Pagination;
using Application.Common.Results;
using Domain.Entities.Advert;
using MediatR;

namespace Application.Features.Adverts.Queries.GetMyAdverts
{


    public sealed record GetMyAdvertsQuery(
        Guid UserId,
        int Page = 1,
        int PageSize = 12
    ) : IRequest<Result<PagedResult<AdvertShortDto>>>;

}
