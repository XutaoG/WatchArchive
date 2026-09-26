using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.UserSessionRepo;

public interface IUserSessionRepository
{
    Task<UserSession?> Create(UserSession userSession);

    Task<UserSession?> GetByRefreshTokenHash(string refreshTokenHash);

    Task<UserSession?> GetById(Guid id);

    Task<UserSession?> UpdateRefreshTokenHash(
        Guid id,
        string newRefreshTokenHash,
        DateTime newExpiresAt
    );

    Task<UserSession?> RevokeRefreshToken(Guid id, DateTime revokedAt);
}
