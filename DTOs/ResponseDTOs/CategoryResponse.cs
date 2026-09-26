namespace WatchArchive.Server.DTOs.ResponseDTOs;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required Guid UserId { get; set; }
}
