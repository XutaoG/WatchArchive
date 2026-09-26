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

    public async Task<List<Category>> GetByUserId(Guid userId)
    {
        return await waDbContext.Categories.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await waDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> UpdateById(Guid id, Category category)
    {
        Category? foundCategory = await GetById(id);

        if (foundCategory == null)
        {
            return null;
        }

        foundCategory.Name = category.Name;
        await waDbContext.SaveChangesAsync();

        return foundCategory;
    }

    public async Task<Category?> DeleteById(Guid id)
    {
        Category? foundCategory = await GetById(id);

        if (foundCategory == null)
        {
            return null;
        }

        waDbContext.Categories.Remove(foundCategory);
        await waDbContext.SaveChangesAsync();

        return foundCategory;
    }
}
