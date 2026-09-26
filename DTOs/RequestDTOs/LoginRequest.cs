using System.ComponentModel.DataAnnotations;

namespace WatchArchive.Server.DTOs.RequestDTOs;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
