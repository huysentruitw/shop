using System.Security.Claims;

namespace Shop.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid? FindUserId(this ClaimsPrincipal principal)
    {
        if (principal.Identity is null || !principal.Identity.IsAuthenticated)
            return null;

        var userId = principal.FindFirstValue(ClaimTypes.Name);
        if (userId is null)
            return null;

        if (!Guid.TryParse(userId, out var parsedUserId))
            throw new UnauthorizedAccessException("Invalid name format in claims");

        return parsedUserId;
    }

    public static Guid GetUserId(this ClaimsPrincipal principal)
        => principal.FindUserId() ?? throw new UnauthorizedAccessException("User is not authenticated");
}
