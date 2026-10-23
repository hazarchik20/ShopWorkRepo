namespace BookShopProj.Contracts.Users
{
    public sealed record UpdateUserRequest(
        string? FirstName,
        string? LastName,
        DateOnly? DateOfBirth,
        string? PhoneNumber,
        string? Town
    );
}
