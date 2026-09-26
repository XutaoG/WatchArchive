using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.CategoryRepo;

public interface ICategoryRepository
{
    Task<Category> Create(Category category);

    Task<List<Category>> GetByUserId(Guid userId);

    Task<Category?> GetById(Guid id);

    Task<Category?> UpdateById(Guid id, Category category);

    Task<Category?> DeleteById(Guid id);

    Task<bool> ExistsByNameAndUserId(Guid userId, string name);
}
