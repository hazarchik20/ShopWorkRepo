using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Advart;
using Application.Abstractions.Persistence.User;
using Application.Abstractions.Storage;
using Application.Common.Results;
using Domain.Entities.Advert;
using Domain.ValueObjects.Advart;
using MediatR;


namespace Application.Features.Adverts.Commands.CreateAdvert
{

    public sealed class CreateAdvertCommandHandler
        : IRequestHandler<CreateAdvertCommand, Result<Guid>>
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdvertCommandHandler(
            IAdvertRepository advertRepository,
            IUserRepository userRepository,
            IFileStorageService fileStorage,
            IUnitOfWork unitOfWork)
        {
            _advertRepository = advertRepository;
            _userRepository = userRepository;
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateAdvertCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
            {
                return Result<Guid>.Failure(
                    Error.NotFound(
                        "User.NotFound",
                        "Користувача не знайдено."));
            }

            var now = DateTime.UtcNow;

            var advert = new Advert
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                Town = request.Town?.Trim(),
                Price = request.Price,
                IsPriceNegotiable = request.IsPriceNegotiable,
                StateOfUse = request.StateOfUse,
                AdvertDealType = request.AdvertDealType,
                SellerType = request.SellerType,
                Status = AdvertStatus.Active,
                Genres = request.Genres.ToList(),
                CreatedAt = now,
                UpdatedAt = now
            };

            var savedFiles = new List<string>();

            try
            {
                foreach (var image in request.Images)
                {
                    var path = await _fileStorage.SaveAsync(
                        image.Content,
                        image.ContentType,
                        advert.Id,
                        cancellationToken);

                    savedFiles.Add(path);
                }

                advert.ImagesLinks = savedFiles.ToList();

                await _advertRepository.AddAsync(
                    advert,
                    cancellationToken);

                await _unitOfWork.SaveChangesAsync(
                    cancellationToken);

                return Result<Guid>.Success(advert.Id);
            }
            catch
            {
                // Компенсаційне очищення вже збережених файлів.
                foreach (var path in savedFiles)
                {
                    try
                    {
                        await _fileStorage.DeleteAsync(
                            path,
                            CancellationToken.None);
                    }
                    catch
                    {
                        // Помилку очищення потрібно логувати.
                        // У production бажаний повторний cleanup.
                    }
                }

                throw;
            }
        }
    }

}
