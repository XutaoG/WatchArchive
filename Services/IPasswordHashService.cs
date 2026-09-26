using WatchArchive.Server.Models;

namespace WatchArchive.Server.Services;

public interface IPasswordHashService
{
    string HashPassword(User user, string password);

    bool VerifyPassword(User user, string hashedPassword, string password);
}
