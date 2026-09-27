namespace WatchArchive.Server.DTOs.ResponseDTOs;

public class TagEntryResponse
{
    public Guid RatedEntryId { get; set; }

    public Guid TagId { get; set; }

    public required TagResponse Tag { get; set; }
}
