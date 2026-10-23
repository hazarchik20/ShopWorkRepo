using Domain.ValueObjects.Advart;

namespace BookShopProj.Contracts.Adverts
{
    public sealed record UpdateAdvertRequest(
        string Title,
        string Description,
        decimal Price,
        bool IsPriceNegotiable,
        string? Town,
        AdvertStateOfUse StateOfUse,
        AdvertDealType AdvertDealType,
        AdvertSellerType SellerType,
        List<AdvertGenres> Genres
    );

}
