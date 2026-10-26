using Application.Abstractions.Storage;
using Application.Features.Adverts.Commands.CreateAdvert;
using Application.Features.Adverts.Commands.DeleteAdvert;
using Application.Features.Adverts.Commands.UpdateAdvert;
using Application.Features.Adverts.Commands.UpdateAdvertImages;
using Application.Features.Adverts.Commands.UpdateAdvertStatus;
using Application.Features.Adverts.Queries.GetActiveAdverts;
using Application.Features.Adverts.Queries.GetAdvertById;
using Application.Features.Adverts.Queries.GetCatalogSectionsQuery;
using Application.Features.Adverts.Queries.GetMyAdvertById;
using Application.Features.Adverts.Queries.GetMyAdverts;
using BookShopProj.Contracts.Adverts;
using BookShopProj.Extensions;
using Domain.ValueObjects.Advart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShopProj.Controllers
{


    [ApiController]
    [Route("api/adverts")]
    public sealed class AdvertsController : ControllerBase
    {
        private readonly ISender _sender;

        public AdvertsController(ISender sender)
        {
            _sender = sender;
        }
        // GET api/adverts/{id}
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetAdvertByIdQuery(id);

            var result = await _sender.Send(
                query, cancellationToken);

            return result.ToActionResult();
        }

        // GET api/adverts/my
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyAdverts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            CancellationToken cancellationToken = default)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var query = new GetMyAdvertsQuery(
                userId.Value, page, pageSize);

            var result = await _sender.Send(
                query, cancellationToken);

            return result.ToActionResult();
        }

        // POST api/adverts
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(60 * 1024 * 1024)]
        public async Task<IActionResult> Create(
            [FromForm] CreateAdvertRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var streams = new List<Stream>();

            try
            {
                var images = new List<AdvertUpload>();

                foreach (var file in request.Images)
                {
                    var stream = file.OpenReadStream();
                    streams.Add(stream);

                    images.Add(new AdvertUpload(
                        stream,
                        file.ContentType,
                        file.Length));
                }

                var command = new CreateAdvertCommand(
                    UserId: userId.Value,
                    Title: request.Title,
                    Description: request.Description,
                    Price: request.Price,
                    IsPriceNegotiable: request.IsPriceNegotiable,
                    Town: request.Town,
                    StateOfUse: request.StateOfUse,
                    AdvertDealType: request.AdvertDealType,
                    SellerType: request.SellerType,
                    Genres: request.Genres,
                    Images: images
                );

                var result = await _sender.Send(
                    command, cancellationToken);

                return result.ToActionResult();
            }
            finally
            {
                foreach (var stream in streams)
                    await stream.DisposeAsync();
            }
        }

        // PUT api/adverts/{id}
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateAdvertRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var command = new UpdateAdvertCommand(
                AdvertId: id,
                UserId: userId.Value,
                Title: request.Title,
                Description: request.Description,
                Price: request.Price,
                IsPriceNegotiable: request.IsPriceNegotiable,
                Town: request.Town,
                StateOfUse: request.StateOfUse,
                AdvertDealType: request.AdvertDealType,
                SellerType: request.SellerType,
                Genres: request.Genres
            );

            var result = await _sender.Send(
                command, cancellationToken);

            return result.ToActionResult();
        }

        // PATCH api/adverts/{id}/status
        [HttpPatch("{id:guid}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            [FromBody] UpdateAdvertStatusRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var command = new UpdateAdvertStatusCommand(
                id,
                userId.Value,
                request.Status
            );

            var result = await _sender.Send(
                command, cancellationToken);

            return result.ToActionResult();
        }

        // PUT api/adverts/{id}/images
        [HttpPut("{id:guid}/images")]
        [Authorize]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(60 * 1024 * 1024)]
        public async Task<IActionResult> UpdateImages(
            Guid id,
            [FromForm] UpdateAdvertImagesRequest request,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var streams = new List<Stream>();

            try
            {
                var images = new List<AdvertUpload>();

                foreach (var file in request.NewImages)
                {
                    var stream = file.OpenReadStream();
                    streams.Add(stream);

                    images.Add(new AdvertUpload(
                        stream,
                        file.ContentType,
                        file.Length));
                }

                var command = new UpdateAdvertImagesCommand(
                    AdvertId: id,
                    UserId: userId.Value,
                    KeepImages: request.KeepImages,
                    NewImages: images
                );

                var result = await _sender.Send(
                    command, cancellationToken);

                return result.ToActionResult();
            }
            finally
            {
                foreach (var stream in streams)
                    await stream.DisposeAsync();
            }
        }

        // DELETE api/adverts/{id}
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var command = new DeleteAdvertCommand(
                id, userId.Value);

            var result = await _sender.Send(
                command, cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("my/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetMyAdvertById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var query = new GetMyAdvertByIdQuery(
                id,
                userId.Value
            );

            var result = await _sender.Send(query, cancellationToken);

            return result.ToActionResult();
        }
        // REPLACE existing [HttpGet] public catalog action with this one (do not leave two GET /api/adverts):
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveAdverts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] string? search = null,
            [FromQuery] AdvertStateOfUse? stateOfUse = null,
            [FromQuery] string sort = "newest",
            CancellationToken cancellationToken = default)
        {
            var query = new GetActiveAdvertsQuery(page, pageSize, search, stateOfUse, sort);
            var result = await _sender.Send(query, cancellationToken);
            return result.ToActionResult();
        }

        [HttpGet("sections")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCatalogSections(
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new GetCatalogSectionsQuery(),
                cancellationToken
            );

            return result.ToActionResult();
        }



    }

}
