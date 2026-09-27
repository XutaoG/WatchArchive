namespace WatchArchive.Server.DTOs.ResponseDTOs;

public class RatedEntryResponse
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public decimal OverallRating { get; set; }

    public Guid ThumbnailId { get; set; }

    public required ThumbnailResponse Thumbnail { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public List<CategoryEntryResponse> CategoryEntries { get; set; } = [];

    public List<TagEntryResponse> TagEntries { get; set; } = [];
}
