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

namespace Application.Features.Adverts.Commands.DeleteAdvert
{
    public sealed class DeleteAdvertCommandHandler
        : IRequestHandler<DeleteAdvertCommand, Result>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAdvertCommandHandler(
            IAdvertRepository advertRepository,
            IUnitOfWork unitOfWork)
        {
            _advertRepository = advertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteAdvertCommand request,
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
                        "Ви не можете видалити чуже оголошення."));
            }

            // Повторне видалення
            if (advert.Status == AdvertStatus.Deleted)
            {
                return Result.Success();
            }

            // Забороняємо видаляти зарезервовані оголошення
            if (advert.Status == AdvertStatus.Reserved)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Advert.CannotDelete",
                        "Неможливо видалити зарезервоване оголошення."));
            }

            var now = DateTime.UtcNow;

            advert.Status = AdvertStatus.Deleted;
            advert.DeletedAt = now;
            advert.UpdatedAt = now;

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result.Success();
        }
    }

}
