using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.RatedEntryRepo;

public interface IRatedEntryRepository
{
    Task<RatedEntry?> GetByUserIdAndId(Guid userId, Guid id);

    Task<List<RatedEntry>> GetAllByUserId(Guid userId);

    Task<RatedEntry> Create(RatedEntry newRatedEntry);

    Task<RatedEntry?> DeleteByUserIdAndId(Guid userId, Guid id);
}
