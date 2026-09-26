namespace WatchArchive.Server.DTOs.ResponseDTOs;

public class UserResponse
{
    public Guid Id { get; set; }

    public required string Username { get; set; }
}
