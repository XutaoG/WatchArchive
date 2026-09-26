using System.Security.Cryptography;
using System.Text;

namespace WatchArchive.Server.Services;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string HashToken(string token)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(hash);
    }

    public bool VerifyToken(string token, string hash)
    {
        string tokenHash = HashToken(token);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromHexString(tokenHash),
            Convert.FromHexString(hash)
        );
    }
}
