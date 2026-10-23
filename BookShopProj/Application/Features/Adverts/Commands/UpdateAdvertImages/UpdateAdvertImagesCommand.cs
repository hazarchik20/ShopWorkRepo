using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Storage;
using Application.Common.Results;
using MediatR;

namespace Application.Features.Adverts.Commands.UpdateAdvertImages
{

    public sealed record UpdateAdvertImagesCommand(
        Guid AdvertId,
        Guid UserId,
        List<string> KeepImages,
        List<AdvertUpload> NewImages
    ) : IRequest<Result>;

}
