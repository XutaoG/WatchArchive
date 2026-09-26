using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WatchArchive.Server.Models;

[Table("users")]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username", TypeName = "citext")]
    [MaxLength(24)]
    public required string Username { get; set; }

    [Column("password_hash")]
    public required string PasswordHash { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
