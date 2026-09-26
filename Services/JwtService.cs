using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Services;

public sealed class JwtService(IAppConfiguration appConfiguration) : IJwtService
{
    public string GenerateAccessToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        JwtSecurityToken token = new(
            issuer: appConfiguration.JwtIssuer,
            audience: appConfiguration.JwtAudience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ],
            expires: DateTime.UtcNow.AddMinutes(appConfiguration.AccessTokenLifetimeMinutes),
            signingCredentials: new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.JwtSigningKey)),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Guid? GetUserIdFromClaims(List<Claim> claims)
    {
        Claim? subClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (subClaim == null)
        {
            return null;
        }

        if (Guid.TryParse(subClaim.Value, out Guid userId))
        {
            return userId;
        }

        return null;
    }
}
