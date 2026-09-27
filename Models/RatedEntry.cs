using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WatchArchive.Server.Models;

[Table("rated_entries")]
[Index(nameof(ThumbnailId), IsUnique = true)]
public class RatedEntry
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Column("overall_rating", TypeName = "decimal(3, 1)")]
    public decimal OverallRating { get; set; }

    [ForeignKey(nameof(Thumbnail))]
    [Column("thumbnail_id")]
    public Guid ThumbnailId { get; set; }

    public Thumbnail Thumbnail { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<CategoryEntry> CategoryEntries { get; set; } = [];

    public ICollection<TagEntry> TagEntries { get; set; } = [];
}
