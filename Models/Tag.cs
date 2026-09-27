using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WatchArchive.Server.Models;

[Table("tags")]
[Index(nameof(UserId), nameof(Name), IsUnique = true)]
public class Tag
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name", TypeName = "citext")]
    [MaxLength(32)]
    public required string Name { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public required Guid UserId { get; set; }

    public required User User { get; set; }
}
