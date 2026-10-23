using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Advart;
using Application.Abstractions.Storage;
using Application.Common.Results;
using Domain.ValueObjects.Advart;
using MediatR;


namespace Application.Features.Adverts.Commands.UpdateAdvertImages
{

    public sealed class UpdateAdvertImagesCommandHandler
        : IRequestHandler<UpdateAdvertImagesCommand, Result>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAdvertImagesCommandHandler(
            IAdvertRepository advertRepository,
            IFileStorageService fileStorage,
            IUnitOfWork unitOfWork)
        {
            _advertRepository = advertRepository;
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateAdvertImagesCommand request,
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

            var existingImages = advert.ImagesLinks.ToList();

            // Користувач може залишати лише власні фото
            if (request.KeepImages.Any(
                path => !existingImages.Contains(path)))
            {
                return Result.Failure(
                    Error.BadRequest(
                        "Advert.InvalidImages",
                        "Список містить фотографії, які не належать оголошенню."));
            }

            var imagesToDelete = existingImages
                .Except(request.KeepImages)
                .ToList();

            var uploadedImages = new List<string>();

            try
            {
                foreach (var image in request.NewImages)
                {
                    var path = await _fileStorage.SaveAsync(
                        image.Content,
                        image.ContentType,
                        advert.Id,
                        cancellationToken);

                    uploadedImages.Add(path);
                }

                advert.ImagesLinks = request.KeepImages
                    .Concat(uploadedImages)
                    .ToList();

                advert.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                foreach (var path in uploadedImages)
                {
                    try
                    {
                        await _fileStorage.DeleteAsync(
                            path,
                            CancellationToken.None);
                    }
                    catch
                    {
                        // Тут варто додати ILogger та механізм
                        // повторного очищення файлів.
                    }
                }

                throw;
            }

            // Видаляємо старі файли лише після збереження БД
            foreach (var path in imagesToDelete)
            {
                try
                {
                    await _fileStorage.DeleteAsync(
                        path,
                        CancellationToken.None);
                }
                catch
                {
                    // Оголошення вже оновлене в БД.
                    // Помилку логуватимемо й повторимо
                    // очищення окремо.
                }
            }

            return Result.Success();
        }
    }

}
