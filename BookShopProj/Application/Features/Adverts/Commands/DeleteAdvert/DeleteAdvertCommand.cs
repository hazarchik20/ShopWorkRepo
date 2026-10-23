using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using MediatR;

namespace Application.Features.Adverts.Commands.DeleteAdvert
{
    public sealed record DeleteAdvertCommand(
        Guid AdvertId,
        Guid UserId
    ) : IRequest<Result>;

}
