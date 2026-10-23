using Domain.ValueObjects.Advart;
using Microsoft.AspNetCore.Http;
namespace BookShopProj.Contracts.Adverts
{
    public sealed class CreateAdvertRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
        public bool IsPriceNegotiable { get; set; }

        public string? Town { get; set; }

        public AdvertStateOfUse StateOfUse { get; set; }
        public AdvertDealType AdvertDealType { get; set; }
        public AdvertSellerType SellerType { get; set; }

        public List<AdvertGenres> Genres { get; set; } = new();
        public List<IFormFile> Images { get; set; } = new();
    }

}
