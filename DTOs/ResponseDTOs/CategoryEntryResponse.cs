namespace WatchArchive.Server.DTOs.ResponseDTOs;

public class CategoryEntryResponse
{
    public Guid RatedEntryId { get; set; }

    public Guid CategoryId { get; set; }

    public required CategoryResponse Category { get; set; }
}
