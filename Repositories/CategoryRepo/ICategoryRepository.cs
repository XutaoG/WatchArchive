using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.CategoryRepo;

public interface ICategoryRepository
{
    Task<Category> Create(Category category);

    Task<List<Category>> GetAllByUserId(Guid userId);

    Task<Category?> GetByUserIdAndId(Guid userId, Guid id);

    Task<Category?> UpdateByUserIdAndId(Guid userId, Guid id, Category category);

    Task<Category?> DeleteByUserIdAndId(Guid userId, Guid id);

    Task<bool> ExistsByNameAndUserId(Guid userId, string name);

    Task<List<Category>> GetAllByUserIdAndIds(Guid userId, List<Guid> ids);
}
