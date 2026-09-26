using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchArchive.Server.Models;

[Table("categories")]
public class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(32)]
    public required string Name { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public required Guid UserId { get; set; }

    public required User User { get; set; }
}
