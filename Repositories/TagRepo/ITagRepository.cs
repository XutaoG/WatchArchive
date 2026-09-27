using WatchArchive.Server.Models;

namespace WatchArchive.Server.Repositories.TagRepo;

public interface ITagRepository
{
    Task<Tag> Create(Tag tag);

    Task<List<Tag>> GetAllByUserId(Guid userId);

    Task<Tag?> GetById(Guid id);

    Task<Tag?> GetByUserIdAndId(Guid userId, Guid id);

    Task<Tag?> UpdateById(Guid id, Tag tag);

    Task<Tag?> DeleteById(Guid id);

    Task<bool> ExistsByNameAndUserId(Guid userId, string name);

    Task<List<Tag>> GetAllByUserIdAndIds(Guid userId, List<Guid> ids);
}
