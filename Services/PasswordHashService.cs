using Microsoft.AspNetCore.Identity;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Services;

public class PasswordHashService(PasswordHasher<User> passwordHasher) : IPasswordHashService
{
    public string HashPassword(User user, string password)
    {
        return passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string hashedPassword, string password)
    {
        PasswordVerificationResult res = passwordHasher.VerifyHashedPassword(
            user,
            hashedPassword,
            password
        );

        return res == PasswordVerificationResult.Success
            || res == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
