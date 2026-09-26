using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Data;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.UserSessionRepo;

public class SqlUserSessionRepository(WatchArchiveDbContext waDbContext) : IUserSessionRepository
{
    public async Task<UserSession?> Create(UserSession userSession)
    {
        await waDbContext.UserSessions.AddAsync(userSession);
        await waDbContext.SaveChangesAsync();
        return userSession;
    }

    public async Task<UserSession?> GetById(Guid id)
    {
        return await waDbContext.UserSessions.FirstOrDefaultAsync(us => us.Id == id);
    }

    public async Task<UserSession?> GetByRefreshTokenHash(string refreshTokenHash)
    {
        return await waDbContext
            .UserSessions.Include(us => us.User)
            .FirstOrDefaultAsync(us => us.RefreshTokenHash == refreshTokenHash);
    }

    public async Task<UserSession?> UpdateRefreshTokenHash(
        Guid id,
        string newRefreshTokenHash,
        DateTime newExpiresAt
    )
    {
        UserSession? foundUserSession = await GetById(id);
        if (foundUserSession == null)
        {
            return null;
        }

        foundUserSession.RefreshTokenHash = newRefreshTokenHash;
        foundUserSession.ExpiresAt = newExpiresAt;
        await waDbContext.SaveChangesAsync();

        return foundUserSession;
    }

    public async Task<UserSession?> RevokeRefreshToken(Guid id, DateTime revokedAt)
    {
        UserSession? foundUserSession = await GetById(id);
        if (foundUserSession == null)
        {
            return null;
        }

        foundUserSession.RevokedAt = revokedAt;
        await waDbContext.SaveChangesAsync();

        return foundUserSession;
    }
}
