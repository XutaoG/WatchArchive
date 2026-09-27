using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Data;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.CategoryRepo;

public class SqlCategoryRepository(WatchArchiveDbContext waDbContext) : ICategoryRepository
{
    public async Task<Category> Create(Category category)
    {
        await waDbContext.Categories.AddAsync(category);
        await waDbContext.SaveChangesAsync();

        return category;
    }

    public async Task<List<Category>> GetAllByUserId(Guid userId)
    {
        return await waDbContext.Categories.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Category?> GetByUserIdAndId(Guid userId, Guid id)
    {
        return await waDbContext.Categories.FirstOrDefaultAsync(c =>
            c.UserId == userId && c.Id == id
        );
    }

    public async Task<Category?> UpdateByUserIdAndId(Guid userId, Guid id, Category category)
    {
        Category? foundCategory = await GetByUserIdAndId(userId, id);

        if (foundCategory == null)
        {
            return null;
        }

        foundCategory.Name = category.Name;
        await waDbContext.SaveChangesAsync();

        return foundCategory;
    }

    public async Task<Category?> DeleteByUserIdAndId(Guid userId, Guid id)
    {
        Category? foundCategory = await GetByUserIdAndId(userId, id);

        if (foundCategory == null)
        {
            return null;
        }

        waDbContext.Categories.Remove(foundCategory);
        await waDbContext.SaveChangesAsync();

        return foundCategory;
    }

    public async Task<bool> ExistsByNameAndUserId(Guid userId, string name)
    {
        return await waDbContext.Categories.AnyAsync(c =>
            c.UserId == userId && EF.Functions.ILike(c.Name, name)
        );
    }

    public async Task<List<Category>> GetAllByUserIdAndIds(Guid userId, List<Guid> ids)
    {
        return await waDbContext
            .Categories.Where(c => ids.Contains(c.Id) && c.UserId == userId)
            .ToListAsync();
    }
}
