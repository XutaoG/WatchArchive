using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Data;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.TagRepo;

public class SqlTagRepository(WatchArchiveDbContext waDbContext) : ITagRepository
{
    public async Task<Tag> Create(Tag tag)
    {
        await waDbContext.Tags.AddAsync(tag);
        await waDbContext.SaveChangesAsync();

        return tag;
    }

    public async Task<List<Tag>> GetAllByUserId(Guid userId)
    {
        return await waDbContext.Tags.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Tag?> GetByUserIdAndId(Guid userId, Guid id)
    {
        return await waDbContext.Tags.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);
    }

    public async Task<Tag?> UpdateByUserIdAndId(Guid userId, Guid id, Tag tag)
    {
        Tag? foundTag = await GetByUserIdAndId(userId, id);

        if (foundTag == null)
        {
            return null;
        }

        foundTag.Name = tag.Name;
        await waDbContext.SaveChangesAsync();

        return foundTag;
    }

    public async Task<Tag?> DeleteByUserIdAndId(Guid userId, Guid id)
    {
        Tag? foundTag = await GetByUserIdAndId(userId, id);

        if (foundTag == null)
        {
            return null;
        }

        waDbContext.Tags.Remove(foundTag);
        await waDbContext.SaveChangesAsync();

        return foundTag;
    }

    public async Task<bool> ExistsByNameAndUserId(Guid userId, string name)
    {
        return await waDbContext.Tags.AnyAsync(t =>
            t.UserId == userId && EF.Functions.ILike(t.Name, name)
        );
    }

    public async Task<List<Tag>> GetAllByUserIdAndIds(Guid userId, List<Guid> ids)
    {
        return await waDbContext
            .Tags.Where(t => ids.Contains(t.Id) && t.UserId == userId)
            .ToListAsync();
    }
}
