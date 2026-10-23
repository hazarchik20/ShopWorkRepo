using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Advart;
using Application.Common.Results;
using Domain.ValueObjects.Advart;
using MediatR;

namespace Application.Features.Adverts.Commands.UpdateAdvertStatus
{
    public sealed class UpdateAdvertStatusCommandHandler
        : IRequestHandler<UpdateAdvertStatusCommand, Result>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAdvertStatusCommandHandler(
            IAdvertRepository advertRepository,
            IUnitOfWork unitOfWork)
        {
            _advertRepository = advertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateAdvertStatusCommand request,
            CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetByIdAsync(
                request.AdvertId,
                cancellationToken);

            if (advert is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Advert.NotFound",
                        "Оголошення не знайдено."));
            }

            if (advert.UserId != request.UserId)
            {
                return Result.Failure(
                    Error.Forbidden(
                        "Advert.Forbidden",
                        "Ви не можете змінювати чуже оголошення."));
            }

            if (advert.Status == request.Status)
            {
                return Result.Success();
            }

            bool canChange = advert.Status switch
            {
                AdvertStatus.Active =>
                    request.Status is AdvertStatus.Reserved
                        or AdvertStatus.Sold
                        or AdvertStatus.Hidden,

                AdvertStatus.Reserved =>
                    request.Status is AdvertStatus.Active
                        or AdvertStatus.Sold
                        or AdvertStatus.Hidden,

                AdvertStatus.Hidden =>
                    request.Status == AdvertStatus.Active,

                _ => false
            };

            if (!canChange)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Advert.InvalidStatusTransition",
                        "Неможливо змінити статус оголошення."));
            }

            advert.Status = request.Status;
            advert.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }

}
