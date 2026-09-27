using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WatchArchive.Server.Models;

[Table("category_entries")]
[PrimaryKey(nameof(RatedEntryId), nameof(CategoryId))]
public class CategoryEntry
{
    [Column("rated_entry_id")]
    public Guid RatedEntryId { get; set; }

    public RatedEntry RatedEntry { get; set; } = null!;

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}
