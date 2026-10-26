using Application.Abstractions.Persistence.Advart;
using Application.Common.Results;
using Application.Features.Adverts.DTOs;
using Application.Features.Adverts.Mappers;
using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Adverts.Queries.GetCatalogSectionsQuery
{
    public sealed record GetCatalogSectionsQuery
        : IRequest<Result<IReadOnlyList<CatalogSectionDto>>>;




}
