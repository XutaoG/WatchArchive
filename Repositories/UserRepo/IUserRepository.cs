using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.UserRepo;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);

    Task<User?> Create(User user);

    Task<User?> AuthenticateUser(string username, string password);
}
