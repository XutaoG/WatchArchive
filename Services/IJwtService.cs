using System.Security.Claims;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    Guid? GetUserIdFromClaims(List<Claim> claims);
}
