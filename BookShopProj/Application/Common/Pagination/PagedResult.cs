using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{

    public sealed record PagedResult<T>(
        IReadOnlyList<T> Items,
        int Page,
        int PageSize,
        int TotalCount)
    {
        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);
    }

}
