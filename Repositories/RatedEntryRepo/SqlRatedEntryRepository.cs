using Microsoft.EntityFrameworkCore;
using WatchArchive.Server.Data;
using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.RatedEntryRepo;

public class SqlRatedEntryRepository(WatchArchiveDbContext waDbContext) : IRatedEntryRepository
{
    public async Task<RatedEntry?> GetByUserIdAndId(Guid userId, Guid id)
    {
        return await waDbContext
            .RatedEntries.Include(re => re.Thumbnail)
            .Include(re => re.CategoryEntries)
                .ThenInclude(ce => ce.Category)
            .Include(re => re.TagEntries)
                .ThenInclude(t => t.Tag)
            .FirstOrDefaultAsync(re => re.Id == id && re.UserId == userId);
    }

    public async Task<List<RatedEntry>> GetAllByUserId(Guid userId)
    {
        return await waDbContext
            .RatedEntries.Include(re => re.Thumbnail)
            .Include(re => re.CategoryEntries)
                .ThenInclude(ce => ce.Category)
            .Include(re => re.TagEntries)
                .ThenInclude(t => t.Tag)
            .Where(re => re.UserId == userId)
            .ToListAsync();
    }

    public async Task<RatedEntry> Create(RatedEntry newRatedEntry)
    {
        await waDbContext.RatedEntries.AddAsync(newRatedEntry);
        await waDbContext.SaveChangesAsync();

        return newRatedEntry;
    }

    public async Task<RatedEntry?> DeleteByUserIdAndId(Guid userId, Guid id)
    {
        RatedEntry? foundEntry = await GetByUserIdAndId(userId, id);
        if (foundEntry == null)
        {
            return null;
        }

        waDbContext.RatedEntries.Remove(foundEntry);
        await waDbContext.SaveChangesAsync();

        return foundEntry;
    }
}
