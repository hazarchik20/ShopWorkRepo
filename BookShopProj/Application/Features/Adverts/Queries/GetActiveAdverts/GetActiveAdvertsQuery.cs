using Application.Common.Pagination;
using Application.Common.Results;
using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Queries.GetActiveAdverts
{
    public sealed record GetActiveAdvertsQuery(
        int Page = 1,
        int PageSize = 12,
        string? Search = null,
        AdvertStateOfUse? StateOfUse = null,
        string Sort = "newest"
    ) : IRequest<Result<PagedResult<AdvertShortDto>>>;

}
