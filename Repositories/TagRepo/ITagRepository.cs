using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.TagRepo;

public interface ITagRepository
{
    Task<Tag> Create(Tag tag);

    Task<List<Tag>> GetAllByUserId(Guid userId);

    Task<Tag?> GetByUserIdAndId(Guid userId, Guid id);

    Task<Tag?> UpdateByUserIdAndId(Guid userId, Guid id, Tag tag);

    Task<Tag?> DeleteByUserIdAndId(Guid userId, Guid id);

    Task<bool> ExistsByNameAndUserId(Guid userId, string name);

    Task<List<Tag>> GetAllByUserIdAndIds(Guid userId, List<Guid> ids);
}
