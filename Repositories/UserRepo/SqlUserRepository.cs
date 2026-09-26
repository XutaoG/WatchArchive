using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Data;
using WatchArchive.Server.Models;
using WatchArchive.Server.Services;

namespace WatchArchive.Server.Repositories.UserRepo;

public class SqlUserRepository(
    WatchArchiveDbContext waDbContext,
    IPasswordHashService passwordHashService
) : IUserRepository
{
    public async Task<User?> GetById(Guid id)
    {
        return await waDbContext.Users.FirstOrDefaultAsync((u) => u.Id == id);
    }

    public async Task<User?> Create(User user)
    {
        // Check if user with same username already exists
        User? foundUser = await waDbContext.Users.FirstOrDefaultAsync(
            (u) => EF.Functions.ILike(u.Username, user.Username)
        );

        if (foundUser != null)
        {
            return null;
        }

        // Add user
        await waDbContext.Users.AddAsync(user);
        await waDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> AuthenticateUser(string username, string password)
    {
        User? foundUser = await waDbContext.Users.FirstOrDefaultAsync(
            (u) => EF.Functions.ILike(u.Username, username)
        );

        // Check user existence
        if (foundUser == null)
        {
            return null;
        }

        // Verify password
        if (passwordHashService.VerifyPassword(foundUser, foundUser.PasswordHash, password))
        {
            return foundUser;
        }

        return null;
    }
}
