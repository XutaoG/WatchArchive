using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models;

[Table("rated_entries")]
public class RatedEntry
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(128)]
    public required string Name { get; set; }

    [Column("overall_rating", TypeName = "decimal(3, 1)")]
    public decimal OverallRating { get; set; }

    [ForeignKey(nameof(Thumbnail))]
    [Column("thumbnail_id")]
    public Guid ThumbnailId { get; set; }

    public required Thumbnail Thumbnail { get; set; }

    [ForeignKey(nameof(Category))]
    [Column("category_id")]
    public Guid? CategoryId { get; set; }

    public Category? Category { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public required Guid UserId { get; set; }

    public required User User { get; set; }
}
