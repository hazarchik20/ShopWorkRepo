using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries.GetMyOrders
{
    using Application.Common.Results;
    using global::Application.Common.Pagination;
    using global::Application.Features.Order.DTOs;
    using MediatR;


    public sealed record GetMyOrdersQuery(
        Guid UserId,
        int Page = 1,
        int PageSize = 12,
        bool Sales = false
    ) : IRequest<Result<PagedResult<OrderDto>>>;

}
