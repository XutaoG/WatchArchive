using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models;

[Table("user_sessions")]
public class UserSession
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public required Guid UserId { get; set; }

    public required User User { get; set; }

    [Column("refresh_token_hash")]
    [MaxLength(128)]
    public required string RefreshTokenHash { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [Column("revoked_at")]
    public DateTime? RevokedAt { get; set; }
}
