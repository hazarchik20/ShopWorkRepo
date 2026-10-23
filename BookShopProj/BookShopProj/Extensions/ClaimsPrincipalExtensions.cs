using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookShopProj.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(
            this ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(
                JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out var id)
                ? id
                : null;
        }
    }
}
