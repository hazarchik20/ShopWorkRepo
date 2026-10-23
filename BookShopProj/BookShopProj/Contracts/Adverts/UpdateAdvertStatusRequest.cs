using Domain.ValueObjects.Advart;

namespace BookShopProj.Contracts.Adverts
{
    public sealed record UpdateAdvertStatusRequest(
        AdvertStatus Status
    );

}
