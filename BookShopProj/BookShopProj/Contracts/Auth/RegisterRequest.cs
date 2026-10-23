namespace BookShopProj.Contracts.Auth
{
    public sealed record RegisterRequest(
        string Name,
        string Email,
        string Password
    );

}
