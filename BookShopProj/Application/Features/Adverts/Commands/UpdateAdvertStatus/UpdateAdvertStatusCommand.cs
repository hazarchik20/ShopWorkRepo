using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;
using Domain.ValueObjects.Advart;
using MediatR;

namespace Application.Features.Adverts.Commands.UpdateAdvertStatus
{


    public sealed record UpdateAdvertStatusCommand(
        Guid AdvertId,
        Guid UserId,
        AdvertStatus Status
    ) : IRequest<Result>;

}
