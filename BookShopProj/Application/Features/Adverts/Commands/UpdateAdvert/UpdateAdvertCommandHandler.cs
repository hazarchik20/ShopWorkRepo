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

namespace Application.Features.Adverts.Commands.UpdateAdvert
{
    public sealed class UpdateAdvertCommandHandler
        : IRequestHandler<UpdateAdvertCommand, Result>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAdvertCommandHandler(
            IAdvertRepository advertRepository,
            IUnitOfWork unitOfWork)
        {
            _advertRepository = advertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateAdvertCommand request,
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
                        "Ви не можете редагувати чуже оголошення."));
            }


            if (advert.Status is
                AdvertStatus.Sold or
                AdvertStatus.Rejected or
                AdvertStatus.Deleted)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Advert.CannotEdit",
                        "Це оголошення не можна редагувати."));
            }

            advert.Title = request.Title.Trim();
            advert.Description = request.Description.Trim();
            advert.Town = request.Town?.Trim();
            advert.Price = request.Price;
            advert.IsPriceNegotiable = request.IsPriceNegotiable;
            advert.StateOfUse = request.StateOfUse;
            advert.AdvertDealType = request.AdvertDealType;
            advert.SellerType = request.SellerType;
            advert.Genres = request.Genres.ToList();
            advert.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
