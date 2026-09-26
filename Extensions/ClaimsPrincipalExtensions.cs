using System.Security.Claims;

namespace WatchArchive.Server.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out Guid id))
        {
            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
        }

        return id;
    }
}
